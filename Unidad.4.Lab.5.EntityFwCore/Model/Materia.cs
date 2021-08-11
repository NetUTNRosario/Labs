namespace Model
{
    public class Materia
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int HsSemanales { get; set; }
        public int HsTotales { get; set; }
        public int PlanId { get; set; }
        public Plan Plan { get; set; }
    }
}