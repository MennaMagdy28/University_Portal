namespace HUP.Core.DTOs.CommonDtos
{
    public class PageDto
    {
        public int PageId { get; set; }
        public string PageName { get; set; }
        public string PageDescription { get; set; }
        public List<string> Permissions { get; set; }
    }
}
