public struct SearchResult
{
    public SearchResult(int x, int z, double perc)
    {
        Coords = (x, z);
        Perc = perc;
    }
    public (int x, int z) Coords { get; set; }
    public double Perc { get; set; }
}