namespace SVBU_Test.Tests
{
    class Elem
    {

        public Elem(string NameIs, string NameALGH, string Type)
        {
            this.NameIs = NameIs;
            this.NameALGH = NameALGH;
            this.Type = Type;
        }
        public string NameIs { get; set; }
        public string NameALGH { get; set; }
        public string Type { get; set; }
    }
}
