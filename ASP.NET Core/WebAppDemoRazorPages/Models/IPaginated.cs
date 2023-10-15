namespace WebAppDemoRazorPages.Models
{
    public interface IPaginated
    {
        int Skip { get; }
        int CountItems { get; }
        int PageSize { get; }
    }
}
