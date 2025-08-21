namespace GCLab;

static class GlobalCache
{
    private static readonly List<WeakReference<byte[]>> _cache = new();
    public static void Add(byte[] data) => _cache.Add(new WeakReference<byte[]>(data));
}
