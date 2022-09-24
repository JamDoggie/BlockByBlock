//----------------------------------------------------------------------------------------
//	Copyright © 2007 - 2022 Tangible Software Solutions, Inc.
//	This class can be used by anyone provided that the copyright notice remains intact.
//
//	This class includes methods to convert Java rectangular arrays (jagged arrays
//	with inner arrays of the same length).
//----------------------------------------------------------------------------------------
using net.minecraft.src;

internal static class RectangularArrays
{
    public static IsoImageBuffer[][] RectangularIsoImageBufferArray(int size1, int size2)
    {
        IsoImageBuffer[][] newArray = new IsoImageBuffer[size1][];
        for (int array1 = 0; array1 < size1; array1++)
        {
            newArray[array1] = new IsoImageBuffer[size2];
        }

        return newArray;
    }

    public static Chunk[][] RectangularChunkArray(int size1, int size2)
    {
        Chunk[][] newArray = new Chunk[size1][];
        for (int array1 = 0; array1 < size1; array1++)
        {
            newArray[array1] = new Chunk[size2];
        }

        return newArray;
    }

    public static int[][] RectangularIntArray(int size1, int size2)
    {
        int[][] newArray = new int[size1][];
        for (int array1 = 0; array1 < size1; array1++)
        {
            newArray[array1] = new int[size2];
        }

        return newArray;
    }

    public static float[][] RectangularFloatArray(int size1, int size2)
    {
        float[][] newArray = new float[size1][];
        for (int array1 = 0; array1 < size1; array1++)
        {
            newArray[array1] = new float[size2];
        }

        return newArray;
    }

    public static double[][] RectangularDoubleArray(int size1, int size2)
    {
        double[][] newArray = new double[size1][];
        for (int array1 = 0; array1 < size1; array1++)
        {
            newArray[array1] = new double[size2];
        }

        return newArray;
    }

    public static sbyte[][] RectangularSbyteArray(int size1, int size2)
    {
        sbyte[][] newArray = new sbyte[size1][];
        for (int array1 = 0; array1 < size1; array1++)
        {
            newArray[array1] = new sbyte[size2];
        }

        return newArray;
    }
}