namespace LfuCache_Test1;

public class LfuCache
{
    private int _minFrequency = 0; 
    private readonly int _length;
    private readonly Dictionary<int, LinkedList<CacheItem>> _frequencyList = new();
    private readonly Dictionary<string, LinkedListNode<CacheItem>> _cache = new();

    public LfuCache(int length)
    {
        _length = length;
    }

    public string? Get(string key)
    {
        if (!_cache.ContainsKey(key))
            return null;

        UpdateFrequency(key);
        
        return _cache[key].Value.CacheValue;
    }
    
    public void Put(string key, string value)
    {
        if (_cache.ContainsKey(key))
        {
            _cache[key].Value.CacheValue = value;
            UpdateFrequency(key);
            return;
        }

        if (_cache.Count == _length)
        {
            _cache.Remove(_frequencyList[_minFrequency].Last.Value.CacheKey);
            _frequencyList[_minFrequency].RemoveLast();
        }

        var node = new LinkedListNode<CacheItem>(
            new CacheItem
            {
                CacheKey = key,
                CacheValue = value,
                Frequency = 1
            });
        
        _cache.Add(key, node);
        
        if(!_frequencyList.ContainsKey(1))
            _frequencyList.Add(1, new LinkedList<CacheItem>());

        _frequencyList[1].AddFirst(node);
        _minFrequency = 1;
    }

    private void UpdateFrequency(string key)
    {
        var node = _cache[key];
        
        _frequencyList[node.Value.Frequency].Remove(node);
        
        if(!_frequencyList.ContainsKey(node.Value.Frequency+1))
            _frequencyList.Add(node.Value.Frequency+1, new LinkedList<CacheItem>());
        
        _frequencyList[node.Value.Frequency+1].AddFirst(node);
        node.Value.Frequency++;

        if (_frequencyList[_minFrequency].Count == 0)
            _minFrequency++;
    }
}