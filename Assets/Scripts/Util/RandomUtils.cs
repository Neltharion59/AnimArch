using System;
using System.Security.Cryptography;

// GPT-generated
public static class DecimalRandom
{
    /// <summary>
    /// Generates a high-precision random decimal within the given range.
    /// </summary>
    /// <param name="rand">Random instance to use.</param>
    /// <param name="min">Lower bound of the range.</param>
    /// <param name="max">Upper bound of the range.</param>
    public static decimal NextDecimalHighPrecision(this Random rand, decimal min, decimal max)
    {
        if (rand == null)
            throw new ArgumentNullException(nameof(rand));

        if (min > max)
            throw new ArgumentException("min must be less than or equal to max.");

        // 96-bit integer required for decimal construction
        byte[] bytes = new byte[12];
        rand.NextBytes(bytes);

        int lo = BitConverter.ToInt32(bytes, 0);
        int mid = BitConverter.ToInt32(bytes, 4);
        int hi = BitConverter.ToInt32(bytes, 8);

        // Create a fraction between 0 and 1 with 28 decimal places
        decimal fraction = new decimal(lo, mid, hi, false, 28);

        return min + fraction * (max - min);
    }

    /// <summary>
    /// Generates a high-precision random decimal using a shared RNG.
    /// </summary>
    private static readonly Random SharedRandom = new Random();

    public static decimal Next(decimal min, decimal max)
        => SharedRandom.NextDecimalHighPrecision(min, max);
}
// GPT-generated
public static class IntRandom
{
    /// <summary>
    /// Generates a random Int32 within the given range using the specified Random instance.
    /// </summary>
    public static int NextInt(this Random rand, int min, int max)
    {
        if (rand == null)
            throw new ArgumentNullException(nameof(rand));
        if (min > max)
            throw new ArgumentException("min must be less than or equal to max.");

        // Random.Next is already unbiased for Int32
        return rand.Next(min, max + 1);  // inclusive upper bound
    }

    /// <summary>
    /// Shared Random instance for convenience.
    /// </summary>
    private static readonly Random SharedRandom = new Random();

    /// <summary>
    /// Generates a random Int32 using a shared Random instance.
    /// </summary>
    public static int Next(int min, int max) =>
        SharedRandom.NextInt(min, max);
}
// GPT-generated
public static class LongRandom
{
    /// <summary>
    /// Generates a random Int64 within the given range using the specified Random instance.
    /// </summary>
    public static long NextLong(this Random rand, long min, long max)
    {
        if (rand == null)
            throw new ArgumentNullException(nameof(rand));
        if (min > max)
            throw new ArgumentException("min must be less than or equal to max.");

        // Range as unsigned to avoid overflow issues
        ulong range = (ulong)(max - min);

        byte[] buffer = new byte[8];
        rand.NextBytes(buffer);
        ulong rand64 = BitConverter.ToUInt64(buffer, 0);

        ulong scaled = rand64 % (range + 1);
        return (long)(min + (long)scaled);
    }

    /// <summary>
    /// Shared Random instance for convenience.
    /// </summary>
    private static readonly Random SharedRandom = new Random();

    /// <summary>
    /// Generates a random Int64 using a shared Random instance.
    /// </summary>
    public static long Next(long min, long max)
        => SharedRandom.NextLong(min, max);
}
// GPT-generated
public static class BoolRandom
{
    /// <summary>
    /// Returns a random boolean using the provided Random instance.
    /// </summary>
    public static bool NextBool(this Random rand)
    {
        if (rand == null)
            throw new ArgumentNullException(nameof(rand));

        // rand.Next(0,2) → 0 or 1
        return rand.Next(0, 2) == 1;
    }

    /// <summary>
    /// Shared Random instance for convenience.
    /// </summary>
    private static readonly Random SharedRandom = new Random();

    /// <summary>
    /// Returns a random boolean using a shared Random instance.
    /// </summary>
    public static bool Next()
    {
        return SharedRandom.NextBool();
    }
}

