namespace ScrumPoker.Business.Abstract
{
	public interface IAvatarService
	{
		Task<List<string>> GetAll();
		Task<List<string>> GetList(int count = 12);
	}
}
