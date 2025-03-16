using ScrumPoker.Business.Abstract;
using ScrumPoker.Helper;

namespace ScrumPoker.Business.Concrete
{
	public class AvatarService : IAvatarService
	{
		public async Task<List<string>> GetAll()
		{
			List<string> list = new List<string>();
			for (int number = 1; number <= 30; number++)
			{
				list.Add("../images/avatars/avatar-" + number.ToString() + ".jpg");
			}
			return list;
		}

		public async Task<List<string>> GetList(int count = 12)
		{
			List<string> list = new List<string>();
			var numberList = RandomHelper.GetRandomNumbers(1, 30, count);
			foreach (var number in numberList)
			{
				list.Add("../images/avatars/avatar-" + number.ToString() + ".jpg");
			}
			return list;
		}
	}
}
