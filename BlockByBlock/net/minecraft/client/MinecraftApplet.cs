using net.minecraft.src;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.ComponentModel;
using System.Threading;

namespace net.minecraft.client
{

	using CanvasMinecraftApplet = net.minecraft.src.CanvasMinecraftApplet;
	using MinecraftAppletImpl = net.minecraft.src.MinecraftAppletImpl;
	using Session = net.minecraft.src.Session;

	public class MinecraftApplet : NativeWindow
	{
		private Minecraft mc;
		private Thread mcThread = null;

		private List<KeyEvent> _events = new(128);
		private List<MouseEvent> _mouseEvents = new(128);

		protected NativeWindowSettings windowSettings { get; set; }

		public static MinecraftApplet mcWindow; // I will be lazy and set static references and YOU CAN'T STOP ME!!!! >:((((

		public MinecraftApplet(NativeWindowSettings settings) : base(settings)
		{
			windowSettings = settings;
			mcWindow = this;
		}

		public virtual void init()
		{
			// PORTING TODO: Launch parameters
			bool fullscreen = false;

			mc = new MinecraftAppletImpl(this, this, this, Size.X, Size.Y, fullscreen);
			mc.minecraftUri = "www.minecraft.net";

			mc.session = new Session("Yammy", "");

			Closing += MinecraftApplet_Closed;
			FocusedChanged += MinecraftApplet_FocusedChanged;
			MouseDown +=MinecraftApplet_MouseDown;
			MouseUp += MinecraftApplet_MouseUp;
			MouseWheel += MinecraftApplet_MouseWheel;
            MouseMove += MinecraftApplet_MouseMove;

			KeyDown += MinecraftApplet_KeyDown;
			KeyUp += MinecraftApplet_KeyUp;
			// PORTING TODO: may need to call mc.SetServer (?) I don't think so, but check what this does for sure.
		}

        

        #region MOUSE INPUT
        private void MinecraftApplet_MouseUp(MouseButtonEventArgs e)
		{
			DoMouseEvent(new MouseEvent(MouseEventType.BUTTON, MouseState.ScrollDelta.Y, mc.MouseScrollDelta, null, null, 
				(int)MouseState.Position.X, (int)MouseState.Position.Y, e.Action, e.Button));
		}

		private void MinecraftApplet_MouseDown(MouseButtonEventArgs e)
		{
			DoMouseEvent(new MouseEvent(MouseEventType.BUTTON, MouseState.ScrollDelta.Y, mc.MouseScrollDelta, null, null, 
				(int)MouseState.Position.X, (int)MouseState.Position.Y, e.Action, e.Button));
			
		}
		private void MinecraftApplet_MouseMove(MouseMoveEventArgs e)
		{
			DoMouseEvent(new MouseEvent(MouseEventType.MOVED, MouseState.ScrollDelta.Y, mc.MouseScrollDelta, (int)e.DeltaX, 
				(int)e.DeltaY, (int)MouseState.Position.X, (int)MouseState.Position.Y, null, null));
		}

		private void MinecraftApplet_MouseWheel(MouseWheelEventArgs e)
		{
			DoMouseEvent(new MouseEvent(MouseEventType.SCROLL, e.OffsetY, (int)e.OffsetY, null,
				null, (int)MouseState.Position.X, (int)MouseState.Position.Y, null, null));
		}

		private void DoMouseEvent(MouseEvent e)
		{
			_mouseEvents.Add(e);
		}
		
		#endregion
        
		#region Keyboard Input
		private void MinecraftApplet_KeyUp(KeyboardKeyEventArgs e)
		{
			if (mc.currentScreen == null || mc.currentScreen.allowUserInput)
				KeyUpDown(new KeyEvent(e, false));
		}

		private void MinecraftApplet_KeyDown(KeyboardKeyEventArgs e)
		{
			if (mc.currentScreen == null || mc.currentScreen.allowUserInput)
				KeyUpDown(new KeyEvent(e, true));
		}

		private void KeyUpDown(KeyEvent e)
		{
			_events.Add(e);
		}

		#endregion

		private void MinecraftApplet_FocusedChanged(OpenTK.Windowing.Common.FocusedChangedEventArgs e)
		{
			if (e.IsFocused)
				start();
			else
				stop();
		}

		private void MinecraftApplet_Closed(CancelEventArgs e)
		{
			destroy();
		}

		public virtual void startMainThread()
		{
			if (mcThread == null)
			{
				mcThread = new Thread(() => mc.run());
				mcThread.Name = "Minecraft main thread";
				mcThread.Start();
			}
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			mc.shutdown();

			try
			{
				mcThread.Join();
			}
			catch (ThreadInterruptedException ex)
			{
				Console.WriteLine(ex.ToString());
				Console.Write(ex.StackTrace);
			}

			base.OnClosing(e);

			Environment.Exit(0);
		}

        public void EnableKeyRepeatingEvents(bool enable)
        {
            // PORTING TODO: Implement
        }

		// Keyboard events
		public virtual bool NextKeyEvent()
		{
			if (_events.Count > 0)
				_events.RemoveAt(0);

			return _events.Count > 0;
		}

		public virtual KeyEvent? CurrentKeyEvent()
		{
			if (_events.Count > 0)
				return _events[0];

			return null;
		}

		// Mouse events
		public virtual bool NextMouseEvent()
		{
			if (_mouseEvents.Count > 0)
				_mouseEvents.RemoveAt(0);

			return _mouseEvents.Count > 0;
		}

		public virtual MouseEvent? CurrentMouseEvent()
		{
			if (_mouseEvents.Count > 0)
				return _mouseEvents[0];

			return null;
		}

		public virtual void start()
		{
			if (mc != null)
			{
				mc.isGamePaused = false;
			}

		}

		public virtual void stop()
		{
			if (mc != null)
			{
				mc.isGamePaused = true;
			}

		}

		public virtual void destroy()
		{
			shutdown();
		}

		public virtual void shutdown()
		{
			if (mcThread != null)
			{
				mc.shutdown();

				try
				{
					mcThread.Join(10000);
				}
				catch (ThreadInterruptedException)
				{
					try
					{
						mc.shutdownMinecraftApplet();
					}
					catch (Exception exception3)
					{
						Console.WriteLine(exception3.ToString());
						Console.Write(exception3.StackTrace);
					}
				}

				mcThread = null;
			}
		}

		public virtual void clearApplet()
		{
			// PORTING TODO: add any stuff for disposing this window here.
		}
	}

	public struct KeyEvent
    {
		public KeyboardKeyEventArgs e;
        public bool isPressed;

        public KeyEvent(KeyboardKeyEventArgs e, bool isPressed)
        {
            this.e = e;
            this.isPressed = isPressed;
        }
    }

	public struct MouseEvent
    {
		public MouseEventType type;
		public float rawScrollDelta;
		public int scrollDelta;
		public int? deltaX;
		public int? deltaY;
		public int posX;
		public int posY;
		public InputAction? buttonAction;
		public MouseButton? button;
        public bool IsPressed => buttonAction == InputAction.Press || buttonAction == InputAction.Repeat;

        public MouseEvent(MouseEventType type, float rawScrollDelta, int scrollDelta, int? deltaX, int? deltaY, int posX, int posY, InputAction? buttonAction, MouseButton? button)
        {
            this.type = type;
            this.rawScrollDelta = rawScrollDelta;
            this.scrollDelta = scrollDelta;
            this.deltaX = deltaX;
            this.deltaY = deltaY;
            this.posX = posX;
            this.posY = posY;
            this.buttonAction = buttonAction;
            this.button = button;
        }
    }

	public enum MouseEventType
    {
		BUTTON,
		SCROLL,
		MOVED
    }
}