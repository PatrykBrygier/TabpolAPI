namespace Tabpol.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public int TabId { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string? Tuning { get; set; }
        public string? Album { get; set; }
        public int? ReleaseYear { get; set; }
        public string? Genre { get; set; }
        public string? Duration { get; set; }
        public string? Difficulty { get; set; }
        public string? Tempo { get; set; }
    }
}
