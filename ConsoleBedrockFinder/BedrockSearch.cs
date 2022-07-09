public class BedrockSearch
{
    public BedrockSearch(ProgressSave save)
    {
        Range = save.Range;
        Pattern = save.Pattern;
        Progress = save.Progress;
        Result = save.Result;
        PathToSave = save.Path;
    } 
    public BedrockSearch(BedrockPattern pattern, SearchRange range, string pathToSave)
    {
        Range = range;
        Pattern = pattern;        
        Progress = new SearchProgress(range.Start.X);
        Result = new List<SearchResult>();
        PathToSave = pathToSave;
    }
    public string PathToSave;
    public bool AutoSave = true;
    public MultiThreading MultiThreading = new MultiThreading(Environment.ProcessorCount);
    public SearchRange Range;
    public BedrockPattern Pattern;
    public SearchProgress Progress;
    public List<SearchResult> Result;
    public bool Working;
    private object @lock = new object();
    public void Start()
    {
        Working = true;
        List<(sbyte, BlockType)> yQueue = GetQueue();
        for (int x = Progress.X; x < Range.End.X; x++)
        {
            Parallel.For(Range.Start.Z, Range.End.Z, MultiThreading.ParallelOptions, z =>
            {
                CalculateChunk(yQueue, x, z);
            });
        }
    }
    private bool CalculateChunk(List<(sbyte, BlockType)> queue, in int x, in int z)
    {        
        for (int incX = 0; incX < 16; incX++)
            for (int incZ = 0; incZ < 16; incZ++)
            {                
                foreach ((sbyte y, BlockType type) in queue)                   
                    foreach ((ushort bx, ushort bz, BlockType block) in Pattern.GetFloor(y).blockList.Where(z => z.block == type))
                        if (!Equals(type, BedrockGen_1_12_2.GetBlock((x << 4) + bx + incX, y, (z << 4) + bz + incZ)))
                            goto StartNexBlock;    
                return true;
                StartNexBlock: { }
            }
        return false;
    }
    private bool Equals(BlockType block, bool isBedrock) => block == BlockType.Bedrock && isBedrock || block == BlockType.Stone && !isBedrock;
    private List<(sbyte, BlockType)> GetQueue()
    {
        List<(sbyte, BlockType)> queue = new List<(sbyte, BlockType)>();
        if (Pattern.ExistedFloors.Any(z => z == 1)) queue.Add((1, BlockType.Stone));
        if (Pattern.ExistedFloors.Any(z => z == 4)) queue.Add((4, BlockType.Bedrock));
        if (Pattern.ExistedFloors.Any(z => z == 2)) queue.Add((2, BlockType.Stone));
        if (Pattern.ExistedFloors.Any(z => z == 3)) queue.Add((3, BlockType.Bedrock));
        if (Pattern.ExistedFloors.Any(z => z == 3)) queue.Add((3, BlockType.Stone));
        if (Pattern.ExistedFloors.Any(z => z == 2)) queue.Add((2, BlockType.Bedrock));
        if (Pattern.ExistedFloors.Any(z => z == 4)) queue.Add((4, BlockType.Stone));
        if (Pattern.ExistedFloors.Any(z => z == 1)) queue.Add((1, BlockType.Bedrock));
        return queue;
    }
    public void Stop()
    {
        Working = false;
    }
}