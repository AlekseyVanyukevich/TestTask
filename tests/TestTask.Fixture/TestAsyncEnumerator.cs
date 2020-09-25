﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestTask.Fixture
{
    internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }
        
        
        public ValueTask DisposeAsync()
        {
            _inner.Dispose();
            return new ValueTask(Task.CompletedTask);
        }

        public ValueTask<bool> MoveNextAsync()
        {
            return new ValueTask<bool>(_inner.MoveNext());
        }

        public T Current => _inner.Current;
    }
}