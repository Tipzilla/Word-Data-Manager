// Represents a node containing word information
public class Node
{
    // Gets the word associated with the node
    public string Word { get; private set; }

    // Gets the length of the word associated with the node
    public int Length { get; private set; }

    // Constructor initializes the Node with a given word
    public Node(string word)
    {
        Word = word;
        Length = word.Length;
    }

    // Overrides the ToString method to provide a string representation of the Node
    public override string ToString()
    {
        return $"Word: {Word}, Length: {Length}";
    }
}