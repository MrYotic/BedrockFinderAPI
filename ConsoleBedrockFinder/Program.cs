internal class Program
{
    static void Main(string[] args)
    {
        Console.InputEncoding = Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Hello, World!");
        
        BedrockPattern pattern = new BedrockPattern(16, 16, 4, 3);

        pattern[4].Set(0, 0, BlockType.Stone);
        pattern[4].Set(0, 1, BlockType.Stone);
        pattern[4].Set(0, 2, BlockType.Stone);
        pattern[4].Set(1, 0, BlockType.Stone);
        pattern[4].Set(1, 1, BlockType.Stone);
        pattern[4].Set(1, 2, BlockType.Stone);
        pattern[4].Set(2, 0, BlockType.Stone);
        pattern[4].Set(2, 1, BlockType.Stone);
        pattern[4].Set(2, 2, BlockType.Stone);

        pattern[3].Set(0, 0, BlockType.Bedrock);
        pattern[3].Set(0, 1, BlockType.Bedrock);
        pattern[3].Set(0, 2, BlockType.Bedrock);
        pattern[3].Set(1, 0, BlockType.Bedrock);
        pattern[3].Set(1, 1, BlockType.Bedrock);
        pattern[3].Set(1, 2, BlockType.Bedrock);
        pattern[3].Set(2, 0, BlockType.Bedrock);
        pattern[3].Set(2, 1, BlockType.Bedrock);
        pattern[3].Set(2, 2, BlockType.Bedrock);

        BedrockSearch search = new BedrockSearch(pattern, new SearchRange(new Vec2i(0, 0), new Vec2i(11718, 16)), "nothing")
        {
            AutoSave = false
        };
        DateTime start = DateTime.Now;
        search.Start();
        Console.WriteLine(DateTime.Now - start);
    }
}
