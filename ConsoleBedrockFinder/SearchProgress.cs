public class SearchProgress
{
    public SearchProgress(int x)
    {
        X = x;
    }
    public int X; //chunks
    public double GetPercent(int start, int end)
    {
        long lStart = (long)start + int.MaxValue;
        long lEnd = (long)end + int.MaxValue;
        long lX = (long)X + int.MaxValue;
        lEnd = lEnd - lStart;
        lX = lX - lStart;
        lStart = 0;
        return Math.Round((double)lX / (lEnd - lStart), 2, MidpointRounding.AwayFromZero);
    }
}