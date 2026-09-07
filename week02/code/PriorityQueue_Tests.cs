using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue items with different priorities: "A" (1), "B" (5), "C" (3).
    // Dequeue all items to verify they are returned in order of highest priority and removed.
    // Expected Result: "B", "C", "A"
    // Defect(s) Found: Dequeue did not remove the item from the queue list, so subsequent calls returned the same item instead of advancing to the next highest priority item.
    public void TestPriorityQueue_HighestPriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 3);

        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue multiple items where some have the same highest priority:
    // "A" (5), "B" (2), "C" (5), "D" (1).
    // Expected Result: "A" should be dequeued before "C" because "A" is closer to the front (FIFO for ties).
    // Defect(s) Found: Dequeue used >= when comparing item priorities. When two items had the same priority, the later item overwrote the earlier one, which broke the FIFO rule for ties.
    public void TestPriorityQueue_TieBreakerFIFO()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 5);
        priorityQueue.Enqueue("B", 2);
        priorityQueue.Enqueue("C", 5);
        priorityQueue.Enqueue("D", 1);

        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("D", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue items where the highest priority item is at the very back of the queue:
    // "A" (1), "B" (2), "C" (3).
    // Expected Result: "C" should be dequeued first since priority 3 is the highest.
    // Defect(s) Found: The for loop used index < _queue.Count - 1, which stopped before checking the last item in the queue. The last element was never considered for highest priority.
    public void TestPriorityQueue_HighestAtEnd()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 2);
        priorityQueue.Enqueue("C", 3);

        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Call Dequeue on an empty queue.
    // Expected Result: InvalidOperationException should be thrown with message "The queue is empty."
    // Defect(s) Found: None. The queue properly threw an InvalidOperationException with the message "The queue is empty."
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                string.Format("Unexpected exception of type {0} caught: {1}",
                    e.GetType(), e.Message)
            );
        }
    }
}