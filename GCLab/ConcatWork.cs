using System.Text;

namespace GCLab;

static class ConcatWork
{
    public static string Fixed()
    {
        var sb = new StringBuilder();
        for (int i = 0; i < 50_000; i++)
            sb.Append(i);
        return sb.ToString();
    }
}
