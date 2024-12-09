using StackExchange.Redis;

namespace Commons;

public static class RedisBloomExtensions
{
	public static async Task BloomReserveAsync(this IDatabaseAsync db, RedisKey key, double errorRate, int initialCapacity)
		=> await db.ExecuteAsync("BF.RESERVE", key, errorRate, initialCapacity);

	public static async Task<bool> BloomAddAsync(this IDatabaseAsync db, RedisKey key, RedisValue value)
		=> (bool)await db.ExecuteAsync("BF.ADD", key, value);

	public static async Task<bool[]> BloomAddAsync(this IDatabaseAsync db, RedisKey key, IEnumerable<RedisValue> values)
		=> (bool[])await db.ExecuteAsync("BF.MADD", values.Cast<object>().Prepend(key).ToArray());

	public static async Task<bool> BloomExistsAsync(this IDatabaseAsync db, RedisKey key, RedisValue value)
		=> (bool)await db.ExecuteAsync("BF.EXISTS", key, value);

	public static async Task<bool[]> BloomExistsAsync(this IDatabaseAsync db, RedisKey key, IEnumerable<RedisValue> values)
			=> (bool[])await db.ExecuteAsync("BF.MEXISTS", values.Cast<object>().Prepend(key).ToArray());

	public static async Task TopKReserveAsync(this IDatabaseAsync db, RedisKey key, int topK, int width = 8, int depth = 7, double decay = 0.9)
		=> await db.ExecuteAsync("TOPK.RESERVE", key, topK, width, depth, decay);

	public static async Task<RedisValue> TopKAddAsync(this IDatabaseAsync db, RedisKey key, RedisValue value)
		=> (RedisValue)await db.ExecuteAsync("TOPK.ADD", key, value);

	public static async Task<RedisValue[]> TopKAddAsync(this IDatabaseAsync db, RedisKey key, IEnumerable<RedisValue> values)
		=> (RedisValue[])await db.ExecuteAsync("TOPK.ADD", values.Cast<object>().Prepend(key).ToArray());

	public static async Task<RedisValue> TopKIncrementAsync(this IDatabaseAsync db, RedisKey key, RedisValue value, int increment)
		=> (RedisValue)await db.ExecuteAsync("TOPK.INCRBY", key, value, increment);

	public static async Task<RedisValue[]> TopKIncrementAsync(this IDatabaseAsync db, RedisKey key, (RedisValue value, int increment)[] increments)
		=> (RedisValue[])await db.ExecuteAsync("TOPK.INCRBY", increments.SelectMany(i => new object[] { i.value, i.increment }).Prepend(key).ToArray());

	public static async Task<RedisValue[]> TopKListAsync(this IDatabaseAsync db, RedisKey key)
		=> (RedisValue[])await db.ExecuteAsync("TOPK.LIST", key);

	public static async Task<bool> TopKQueryAsync(this IDatabaseAsync db, RedisKey key, RedisValue value)
		=> (bool)await db.ExecuteAsync("TOPK.QUERY", key, value);

	public static async Task<bool[]> TopKQueryAsync(this IDatabaseAsync db, RedisKey key, RedisValue[] values)
		=> (bool[])await db.ExecuteAsync("TOPK.QUERY", values.Cast<object>().Prepend(key).ToArray());

	public static async Task<int> TopKCountAsync(this IDatabaseAsync db, RedisKey key, RedisValue value)
		=> (int)await db.ExecuteAsync("TOPK.COUNT", key, value);

	public static async Task<int[]> TopKCountAsync(this IDatabaseAsync db, RedisKey key, RedisValue[] values)
		=> (int[])await db.ExecuteAsync("TOPK.COUNT", values.Cast<object>().Prepend(key).ToArray());
}
