using app1.DTO;

namespace app1.Repo
{
    public static class VillaRepo
    {
        public static List<VillaModelDTO> villaList = new List<VillaModelDTO> 
        {
            new VillaModelDTO { Id=0, Name="Pool view" },
            new VillaModelDTO { Id=1, Name="Beach view"}
        };
        public static List<VillaModelDTO> VillaData()
        {
            return new List<VillaModelDTO>
            {
                new VillaModelDTO(){Id=0, Name="Pool view"},
                new VillaModelDTO(){Id=1, Name="Beach view"}
            };
        }
    }
}
