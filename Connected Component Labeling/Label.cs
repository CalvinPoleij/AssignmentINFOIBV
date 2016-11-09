public class Label
{
    public int id;
    public Label rootLabel;
    public int rank = 0;

    public Label(int id)
    {
        this.id = id;
        rootLabel = this;
    }

    public Label FindRoot()
    {
        Label currentNode = this, rootNode = rootLabel;

        while (currentNode != rootNode)
        {
            currentNode = rootNode;
            rootNode = rootNode.rootLabel;
        }

        rootLabel = rootNode;
        return rootLabel;
    }

    public void Union(Label label)
    {
        if (label.rank < rank)
            label.rootLabel = this;
        else
        {
            rootLabel = label;
            if (rank == label.rank)
                label.rank++;
        }
    }
}