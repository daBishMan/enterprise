namespace Enterprise.Dotnet.API.Helpers;

public class Pagination<T> where T : class
{
  public Pagination(int pageINdex, int pageSize, int count, IReadOnlyList<T> data)
  {
    PageINdex = pageINdex;
    PageSize = pageSize;
    Count = count;
    Data = data;
  }

  public int PageINdex { get; set; }
  public int PageSize { get; set; }
  public int Count { get; set; }
  public IReadOnlyList<T> Data { get; set; }
}
