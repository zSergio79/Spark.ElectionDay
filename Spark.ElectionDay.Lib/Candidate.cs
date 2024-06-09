namespace Spark.ElectionDay.Lib
{
    public class Candidate
    {
        private int id;
        private string name = string.Empty;
        public string Name { get { return name; } set { name = value; } }
        public int Id { get { return id; } set { id = value; } }
    }
}
