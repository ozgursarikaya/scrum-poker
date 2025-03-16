namespace ScrumPoker.Helper
{
	public class RandomHelper
	{
		public static List<int> GetRandomNumbers(int start, int end, int take)
		{
			Random rnd = new Random();
			List<int> randomNumbers = Enumerable.Range(start, end).OrderBy(x => rnd.Next()).Take(take).ToList();
			return randomNumbers;
		}
	}
}
