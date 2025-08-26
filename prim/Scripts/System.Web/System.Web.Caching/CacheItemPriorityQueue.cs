using System.Collections;
using System.Diagnostics;
using System.Threading;

namespace System.Web.Caching;

internal sealed class CacheItemPriorityQueue
{
	private const int INITIAL_HEAP_SIZE = 32;

	private const int HEAP_RESIZE_THRESHOLD = 8192;

	private CacheItem[] heap;

	private int heapSize;

	private int heapCount;

	private ReaderWriterLockSlim queueLock;

	public int Count => heapCount;

	public int Size => heapSize;

	public CacheItem[] Heap => heap;

	public CacheItemPriorityQueue()
	{
		queueLock = new ReaderWriterLockSlim();
	}

	private void ResizeHeap(int newSize)
	{
		CacheItem[] array = heap;
		Array.Resize(ref heap, newSize);
		heapSize = newSize;
		if (array != null)
		{
			((IList)array).Clear();
			array = null;
		}
	}

	private CacheItem[] GetHeapWithGrow()
	{
		if (heap == null)
		{
			heap = new CacheItem[32];
			heapSize = 32;
			heapCount = 0;
			return heap;
		}
		if (heapCount >= heapSize)
		{
			ResizeHeap(heapSize <<= 1);
		}
		return heap;
	}

	private CacheItem[] GetHeapWithShrink()
	{
		if (heap == null)
		{
			return null;
		}
		if (heapSize > 8192)
		{
			int num = heapSize >> 1;
			if (heapCount < num)
			{
				ResizeHeap(num + heapCount / 3);
			}
		}
		return heap;
	}

	public void Enqueue(CacheItem item)
	{
		if (item == null)
		{
			return;
		}
		try
		{
			queueLock.EnterWriteLock();
			CacheItem[] heapWithGrow = GetHeapWithGrow();
			heapWithGrow[heapCount] = item;
			if (heapCount == 0)
			{
				item.PriorityQueueIndex = 0;
			}
			BubbleUp(heapWithGrow, heapCount++);
		}
		finally
		{
			queueLock.ExitWriteLock();
		}
	}

	public CacheItem Dequeue()
	{
		try
		{
			queueLock.EnterWriteLock();
			CacheItem[] heapWithShrink = GetHeapWithShrink();
			if (heapWithShrink == null || heapCount == 0)
			{
				return null;
			}
			CacheItem result = heapWithShrink[0];
			int num = --heapCount;
			heapWithShrink[0] = heapWithShrink[num];
			heapWithShrink[num] = null;
			if (heapCount > 0)
			{
				BubbleDown(heapWithShrink, 0);
			}
			return result;
		}
		finally
		{
			queueLock.ExitWriteLock();
		}
	}

	public bool Update(CacheItem item)
	{
		if (item == null || item.PriorityQueueIndex <= 0 || item.PriorityQueueIndex >= heapCount - 1)
		{
			return false;
		}
		try
		{
			queueLock.EnterWriteLock();
			CacheItem cacheItem = heap[item.PriorityQueueIndex];
			if (cacheItem == null || string.Compare(cacheItem.Key, item.Key, StringComparison.Ordinal) != 0)
			{
				return false;
			}
			int priorityQueueIndex = item.PriorityQueueIndex;
			int num = BubbleUp(heap, priorityQueueIndex);
			if (num > -1 && num >= priorityQueueIndex)
			{
				BubbleDown(heap, num);
			}
		}
		finally
		{
			queueLock.ExitWriteLock();
		}
		return true;
	}

	public CacheItem Peek()
	{
		try
		{
			queueLock.EnterReadLock();
			if (heap == null || heapCount == 0)
			{
				return null;
			}
			return heap[0];
		}
		finally
		{
			queueLock.ExitReadLock();
		}
	}

	private int BubbleDown(CacheItem[] heap, int startIndex)
	{
		int num = startIndex;
		int num2 = startIndex + 1;
		int num3 = startIndex + 2;
		CacheItem cacheItem = heap[num];
		int num4 = ((num3 >= heapCount || heap[num3].ExpiresAt >= heap[num2].ExpiresAt) ? 1 : 2);
		while (true)
		{
			num4 = num;
			num2 = (num << 1) + 1;
			num3 = num2 + 1;
			if (heapCount > num2 && heap[num].ExpiresAt > heap[num2].ExpiresAt)
			{
				num = num2;
			}
			if (heapCount > num3 && heap[num].ExpiresAt > heap[num3].ExpiresAt)
			{
				num = num3;
			}
			if (num == num4)
			{
				break;
			}
			CacheItem cacheItem2 = heap[num];
			heap[num] = heap[num4];
			heap[num].PriorityQueueIndex = num;
			heap[num4] = cacheItem2;
			cacheItem2.PriorityQueueIndex = num4;
		}
		cacheItem.PriorityQueueIndex = num;
		return num;
	}

	private int BubbleUp(CacheItem[] heap, int startIndex)
	{
		if (heapCount <= 1)
		{
			return -1;
		}
		int num = heapCount - 1;
		if (startIndex < 0 || startIndex > num)
		{
			return -1;
		}
		int num2 = startIndex;
		int num3 = num2 - 1 >> 1;
		CacheItem cacheItem = heap[num2];
		while (num2 > 0)
		{
			CacheItem cacheItem2 = heap[num3];
			if (heap[num2].ExpiresAt >= cacheItem2.ExpiresAt)
			{
				break;
			}
			heap[num2] = cacheItem2;
			cacheItem2.PriorityQueueIndex = num2;
			num2 = num3;
			num3 = num2 - 1 >> 1;
		}
		heap[num2] = cacheItem;
		cacheItem.PriorityQueueIndex = num2;
		return num2;
	}

	[Conditional("DEBUG")]
	private void InitDebugMode()
	{
	}

	[Conditional("DEBUG")]
	private void AddSequenceEntry(CacheItem item, EDSequenceEntryType type)
	{
	}

	[Conditional("DEBUG")]
	public void OnItemDisable(CacheItem i)
	{
	}
}
