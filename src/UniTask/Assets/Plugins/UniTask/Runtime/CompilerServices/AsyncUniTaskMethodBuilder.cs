﻿
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace Cysharp.Threading.Tasks.CompilerServices
{
    [StructLayout(LayoutKind.Auto)]
    public struct AsyncUniTaskMethodBuilder
    {
        // cache items.
        internal IMoveNextRunnerPromise runnerPromise;
        Exception ex;

        // 1. Static Create method.
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncUniTaskMethodBuilder Create()
        {
            return default;
        }

        // 2. TaskLike Task property.
        public UniTask Task
        {
            [DebuggerHidden]
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (runnerPromise != null)
                {
                    return runnerPromise.Task;
                }
                else if (ex != null)
                {
                    return UniTask.FromException(ex);
                }
                else
                {
                    return UniTask.CompletedTask;
                }
            }
        }

        // 3. SetException
        [DebuggerHidden]
        public void SetException(Exception exception)
        {
            if (runnerPromise == null)
            {
                ex = exception;
            }
            else
            {
                runnerPromise.SetException(exception);
            }
        }

        // 4. SetResult
        [DebuggerHidden]
        public void SetResult()
        {
            if (runnerPromise != null)
            {
                runnerPromise.SetResult();
            }
        }

        // 5. AwaitOnCompleted
        [DebuggerHidden]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            if (runnerPromise == null)
            {
                MoveNextRunnerPromise<TStateMachine>.SetStateMachine(ref this, ref stateMachine);
            }

            awaiter.OnCompleted(runnerPromise.MoveNext);
        }

        // 6. AwaitUnsafeOnCompleted
        [DebuggerHidden]
        [SecuritySafeCritical]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            if (runnerPromise == null)
            {
                MoveNextRunnerPromise<TStateMachine>.SetStateMachine(ref this, ref stateMachine);
            }

            awaiter.UnsafeOnCompleted(runnerPromise.MoveNext);
        }

        // 7. Start
        [DebuggerHidden]
        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }

        // 8. SetStateMachine
        [DebuggerHidden]
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            // don't use boxed stateMachine.
        }

#if DEBUG || !UNITY_2018_3_OR_NEWER
        // Important for IDE debugger.
        object debuggingId;
        private object ObjectIdForDebugger
        {
            get
            {
                if (debuggingId == null)
                {
                    debuggingId = new object();
                }
                return debuggingId;
            }
        }
#endif
    }

    [StructLayout(LayoutKind.Auto)]
    public struct AsyncUniTaskMethodBuilder<T>
    {
        // cache items.
        internal IMoveNextRunnerPromise<T> runnerPromise;
        Exception ex;
        T result;

        // 1. Static Create method.
        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AsyncUniTaskMethodBuilder<T> Create()
        {
            return default;
        }

        // 2. TaskLike Task property.
        [DebuggerHidden]
        public UniTask<T> Task
        {
            get
            {
                if (runnerPromise != null)
                {
                    return runnerPromise.Task;
                }
                else if (ex != null)
                {
                    return UniTask.FromException<T>(ex);
                }
                else
                {
                    return UniTask.FromResult(result);
                }
            }
        }

        // 3. SetException
        [DebuggerHidden]
        public void SetException(Exception exception)
        {
            if (runnerPromise == null)
            {
                ex = exception;
            }
            else
            {
                runnerPromise.SetException(exception);
            }
        }

        // 4. SetResult
        [DebuggerHidden]
        public void SetResult(T result)
        {
            if (runnerPromise == null)
            {
                this.result = result;
            }
            else
            {
                runnerPromise.SetResult(result);
            }
        }

        // 5. AwaitOnCompleted
        [DebuggerHidden]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            if (runnerPromise == null)
            {
                MoveNextRunnerPromise<TStateMachine, T>.SetStateMachine(ref this, ref stateMachine);
            }

            awaiter.OnCompleted(runnerPromise.MoveNext);
        }

        // 6. AwaitUnsafeOnCompleted
        [DebuggerHidden]
        [SecuritySafeCritical]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            if (runnerPromise == null)
            {
                MoveNextRunnerPromise<TStateMachine, T>.SetStateMachine(ref this, ref stateMachine);
            }

            awaiter.UnsafeOnCompleted(runnerPromise.MoveNext);
        }

        // 7. Start
        [DebuggerHidden]
        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }

        // 8. SetStateMachine
        [DebuggerHidden]
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            // don't use boxed stateMachine.
        }

#if DEBUG || !UNITY_2018_3_OR_NEWER
        // Important for IDE debugger.
        object debuggingId;
        private object ObjectIdForDebugger
        {
            get
            {
                if (debuggingId == null)
                {
                    debuggingId = new object();
                }
                return debuggingId;
            }
        }
#endif

    }
}