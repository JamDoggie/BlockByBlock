using net.minecraft.input;
using net.minecraft.src;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Threading;

namespace net.minecraft.client
{
    
	using MinecraftAppletImpl = net.minecraft.src.MinecraftAppletImpl;
	using Session = net.minecraft.src.Session;

	public class MinecraftApplet : GameWindow
	{
		private Minecraft mc;
		public Thread mcThread = null;

		private List<KeyEvent> _events = new(128);
		private List<MouseEvent> _mouseEvents = new(128);
        private ConcurrentQueue<KeyTypedEvent> _keyTypedEvents = new();

        private MouseEvent? currentMouseEvent = null;
		private KeyEvent? currentKeyEvent = null;
        private KeyTypedEvent? currentKeyTypedEvent = null;

        protected NativeWindowSettings windowSettings { get; set; }

		public static MinecraftApplet mcWindow; // I will be lazy and set static references and YOU CAN'T STOP ME!!!! >:((((

		public static HashSet<string> OpenGLExtensions { get; set; } = new();
        
		public MinecraftApplet(NativeWindowSettings settings) : base(new GameWindowSettings() { }, settings)
		{
			
			windowSettings = settings;
			mcWindow = this;

			// Load extensions
			int count = GL.GetInteger(GetPName.NumExtensions);
			for (var i = 0; i < count; i++)
			{
				var extension = GL.GetString(StringNameIndexed.Extensions, i);
				OpenGLExtensions.Add(extension);
			}

			init();

			//RenderFrame?.Invoke(null);
		}

		public virtual void init()
		{
			VSync = VSyncMode.Off;

			// PORTING TODO: Launch parameters
			bool fullscreen = false;

			mc = new MinecraftAppletImpl(this, this, this, Size.X, Size.Y, fullscreen);
			mc.minecraftUri = "www.minecraft.net";

			mc.session = new Session("Yammy", "");

			Closing += MinecraftApplet_Closed;
			FocusedChanged += MinecraftApplet_FocusedChanged;
			MouseDown += MinecraftApplet_MouseDown;
			MouseUp += MinecraftApplet_MouseUp;
			MouseWheel += MinecraftApplet_MouseWheel;
            MouseMove += MinecraftApplet_MouseMove;

			KeyDown += MinecraftApplet_KeyDown;
			TextInput += MinecraftApplet_TextInput;
			KeyUp += MinecraftApplet_KeyUp;

			// PORTING TODO: may need to call mc.SetServer (?) I don't think so, but check what this does for sure.
		}

		private void MinecraftApplet_TextInput(TextInputEventArgs e)
		{
			char? ch = null;

			if (e.AsString.Length > 0)
				ch = e.AsString[0];

			if (ch != null)
				DoKeyTypedEvent(new KeyTypedEvent(ch.Value));
        }

		#region MOUSE INPUT
		private void MinecraftApplet_MouseUp(MouseButtonEventArgs e)
		{
            if (e.Action != InputAction.Release)
                return;

            DoMouseEvent(new MouseEvent(MouseEventType.BUTTON, 0, 0, null, null, 
				(int)MouseState.Position.X, (int)MouseState.Position.Y, e.Action, e.Button));
		}

		private void MinecraftApplet_MouseDown(MouseButtonEventArgs e)
		{
			if (e.Action != InputAction.Press || !e.IsPressed)
				return;

			DoMouseEvent(new MouseEvent(MouseEventType.BUTTON, 0, 0, null, null, 
				(int)MouseState.Position.X, (int)MouseState.Position.Y, e.Action, e.Button));
			
		}
		private void MinecraftApplet_MouseMove(MouseMoveEventArgs e)
		{
			DoMouseEvent(new MouseEvent(MouseEventType.MOVED, 0, 0, (int)e.DeltaX, 
				(int)e.DeltaY, (int)MouseState.Position.X, (int)MouseState.Position.Y, null, null));
		}

		float previousScrollDelta = 0;

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
				KeyUpDown(new KeyEvent(InputHelper.FromOpenTKKey(e.Key), false));
		}

		private void MinecraftApplet_KeyDown(KeyboardKeyEventArgs e)
		{
			if (mc.currentScreen == null || mc.currentScreen.allowUserInput)
				KeyUpDown(new KeyEvent(InputHelper.FromOpenTKKey(e.Key), true));
		}

		private void KeyUpDown(KeyEvent e)
		{
			_events.Add(e);
		}

        private void DoKeyTypedEvent(KeyTypedEvent e)
        {
			while (_keyTypedEvents.Count > 128)
			{
                _keyTypedEvents.TryDequeue(out _);
            }
                
            _keyTypedEvents.Enqueue(e);
        }

		public void ClearCurrentTypedKey()
		{
            currentKeyTypedEvent = null;
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
			bool hasEvent = false;
            
			if (_events.Count > 0)
			{
				hasEvent = true;
				currentKeyEvent = _events[0];
				_events.RemoveAt(0);
			}

			return hasEvent;
		}

        public virtual bool NextKeyTypedEvent()
        {
            bool hasEvent = false;

			KeyTypedEvent e;
            if (_keyTypedEvents.TryDequeue(out e))
            {
				hasEvent = true;
                currentKeyTypedEvent = e;
            }

			if (!hasEvent)
				currentKeyEvent = null;

            return hasEvent;
        }

        public virtual void ClearKeyTypeQueue()
		{
            _keyTypedEvents.Clear();
			currentKeyEvent = null;
        }

        public virtual KeyEvent? CurrentKeyEvent()
		{
			return currentKeyEvent;
		}

        public virtual KeyTypedEvent? CurrentKeyTypedEvent()
        {
            return currentKeyTypedEvent;
        }

        // Mouse events
        public virtual bool NextMouseEvent()
		{
			bool hasEvent = false;

			if (_mouseEvents.Count > 0)
            {
				hasEvent = true;
				currentMouseEvent = _mouseEvents[0];
				_mouseEvents.RemoveAt(0);
			}


			return hasEvent;
		}

		public virtual MouseEvent? CurrentMouseEvent()
		{
			return currentMouseEvent;
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
        public bool IsPressed;
		public KeyCode Key;

        public KeyEvent(KeyCode key, bool isPressed)
        {
            IsPressed = isPressed;
			Key = key;
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

	public struct KeyTypedEvent
	{
        public char keyChar;

		public KeyTypedEvent(char keyChar)
		{
			this.keyChar = keyChar;
		}
	}

	public enum MouseEventType
    {
		BUTTON,
		SCROLL,
		MOVED
    }
}