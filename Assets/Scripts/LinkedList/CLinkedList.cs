using System;
using System.Collections.Generic;
using System.Text;
using ADT;

namespace CustomLinkedList
{
    class CLinkedList<T> : IDynamicList<T>
    {
        private class ListNode
        {
            public T data;
            public ListNode nextNode;

            public ListNode() 
            {

            }
            public ListNode(T newData, ListNode newNextNode = null)
            {
                data = newData;
                nextNode = newNextNode;
            }
        }

        private int count;
        private ListNode head;
        private ListNode tail;

        public int Count 
        { 
            get
            {
                return count;
            }
        }
        public T LastElement
        {
            get
            {
                return tail.data;
            }
        }
        public T FirstElement
        {
            get
            {
                return head.data;
            }
        }

        public int IndexOf(T item)
        {
            ListNode currentNode = head;
            for (int i = 0; i < count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(currentNode.data, item))
                {
                    return i;
                }
                currentNode = currentNode.nextNode;
            }
            return -1;
        }
        public bool Contains(T item)
        {
            ListNode currentNode = head;
            for (int i = 0; i < count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(currentNode.data, item))
                {
                    return true;
                }
                currentNode = currentNode.nextNode;
            }
            return false;
        }
        public void Add(T item)
        {
            ListNode newNode = new ListNode(item);

            // If the list is empty the new element becomes both head & tail.
            if(count == 0)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.nextNode = newNode;
                tail = newNode;
            }
            count++;
        }
        public void Insert(int index, T item)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException();
            }

            if (index == 0)
            {
                ListNode newNode = new ListNode(item, head);
                head = newNode;
                count++;
            }
            else if (index == count - 1)
            {
                Add(item);
            }
            else
            {
                ListNode oldNode = GetListNode(index - 1);
                ListNode newNode = new ListNode(item, oldNode.nextNode);
                oldNode.nextNode = newNode;
                count++;
            }
        }
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException();
            }

            if (index == 0)
            {
                ListNode tempNode = head;
                head = tempNode.nextNode;
                tempNode.nextNode = null;
            }
            else if (index == count - 1)
            {
                ListNode tempNode = GetListNode(count - 2);
                tempNode.nextNode = null;
                tail = tempNode;
            }
            else
            {
                ListNode nodeBefore = GetListNode(index - 1);
                ListNode tempNode = nodeBefore.nextNode;
                nodeBefore.nextNode = tempNode.nextNode;
                tempNode.nextNode = null;
            }
            count--;
        }
        public bool Remove(T item)
        {
            ListNode currentNode = head;
            ListNode lastNode = head;
            for (int i = 0; i < count; i++)
            {
                //EqualityComparer<T>.Default.Equals(currentNode.data, item)
                if (object.Equals(currentNode.data, item))
                {
                    if(currentNode == head)
                    {
                        head = currentNode.nextNode;
                    }
                    if(currentNode == tail)
                    {
                        tail = lastNode;
                    }

                    lastNode.nextNode = currentNode.nextNode;
                    count--;

                    if(count == 0)
                    {
                        head = null;
                        tail = null;
                    }
                    return true;
                }
                lastNode = currentNode;
                currentNode = currentNode.nextNode;
            }
            return false;
        }
        public void Clear()
        {
            ListNode currentNode = head;
            while(currentNode != null)
            {
                ListNode temp = currentNode;
                currentNode = currentNode.nextNode;
                temp.nextNode = null;
            }
            head = null;
            tail = null;
            count = 0;
        }
        public void CopyTo(T[] target, int index)
        {

        }
        public T this[int index]
        {
            get
            {
                if(index < 0 || index >= count)
                {
                    throw new IndexOutOfRangeException("Index out of rangeeeee.");
                }

                ListNode currentNode = head;
                for(int i = 0; i < index; i++)
                {
                    currentNode = currentNode.nextNode;                   
                }
                return currentNode.data;
            }
            set
            {
                if (index < 0 || index >= count)
                {
                    throw new IndexOutOfRangeException("Index out of rangeeeee.");
                }

                ListNode currentNode = head;
                for (int i = 0; i < index; i++)
                {
                    currentNode = currentNode.nextNode;
                }
                currentNode.data = value;
            }
        }

        // HELPER FUNCTIONS START -----------------
        private ListNode GetListNode(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException();
            }

            ListNode currentNode = head;
            for (int i = 0; i < index; i++)
            {
                currentNode = currentNode.nextNode;
            }
            return currentNode;
        }
    }
}
