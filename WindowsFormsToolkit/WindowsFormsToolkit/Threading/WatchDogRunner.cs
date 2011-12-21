using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WindowsFormsToolkit.Threading
{
    public class WatchDogRunner
    {
        private AutoResetEvent evnt = new AutoResetEvent(false);

        #region Constructors
        /// <summary>
        /// Do the action until timeout.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public bool DoIt(Action action, TimeSpan timeout) {
            return this.DoItImp(action, null, timeout);
        }

        /// <summary>
        /// Do the action until timeout.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public bool DoIt<T1>(Action<T1> action, object[] parameters, TimeSpan timeout)
        {
            return this.DoItImp(action, parameters, timeout);
        }

        /// <summary>
        /// Do the action until timeout.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public bool DoIt<T1, T2>(Action<T1, T2> action, object[] parameters, TimeSpan timeout)
        {
            return this.DoItImp(action, parameters, timeout);
        }

        /// <summary>
        /// Do the action until timeout.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public bool DoIt<T1, T2, T3>(Action<T1, T2, T3> action, object[] parameters, TimeSpan timeout)
        {
            return this.DoItImp(action, parameters, timeout);
        }

        /// <summary>
        /// Do the action until timeout.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public bool DoIt<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, object[] parameters, TimeSpan timeout)
        {
            return this.DoItImp(action, parameters, timeout);
        }

        /// <summary>
        /// Do the action until timeout.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="T5">The type of the 5.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public bool DoIt<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action, object[] parameters, TimeSpan timeout)
        {
            return this.DoItImp(action, parameters, timeout);
        }

        /// <summary>
        /// Do the action until timeout.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="T5">The type of the 5.</typeparam>
        /// <typeparam name="T6">The type of the 6.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public bool DoIt<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action, object[] parameters, TimeSpan timeout)
        {
            return this.DoItImp(action, parameters, timeout);
        }

        /// <summary>
        /// Do the action until timeout.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="T5">The type of the 5.</typeparam>
        /// <typeparam name="T6">The type of the 6.</typeparam>
        /// <typeparam name="T7">The type of the 7.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public bool DoIt<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action, object[] parameters, TimeSpan timeout)
        {
            return this.DoItImp(action, parameters, timeout);
        }

        /// <summary>
        /// Do the action until timeout.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="T5">The type of the 5.</typeparam>
        /// <typeparam name="T6">The type of the 6.</typeparam>
        /// <typeparam name="T7">The type of the 7.</typeparam>
        /// <typeparam name="T8">The type of the 8.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public bool DoIt<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action, object[] parameters, TimeSpan timeout)
        {
            return this.DoItImp(action, parameters, timeout);
        }

        /// <summary>
        /// Do the action until timeout.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="T5">The type of the 5.</typeparam>
        /// <typeparam name="T6">The type of the 6.</typeparam>
        /// <typeparam name="T7">The type of the 7.</typeparam>
        /// <typeparam name="T8">The type of the 8.</typeparam>
        /// <typeparam name="T9">The type of the 9.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public bool DoIt<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action, object[] parameters, TimeSpan timeout)
        {
            return this.DoItImp(action, parameters, timeout);
        }

        /// <summary>
        /// Do the action until timeout.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="T5">The type of the 5.</typeparam>
        /// <typeparam name="T6">The type of the 6.</typeparam>
        /// <typeparam name="T7">The type of the 7.</typeparam>
        /// <typeparam name="T8">The type of the 8.</typeparam>
        /// <typeparam name="T9">The type of the 9.</typeparam>
        /// <typeparam name="T10">The type of the 10.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public bool DoIt<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action, object[] parameters, TimeSpan timeout)
        {
            return this.DoItImp(action, parameters, timeout);
        }

        /// <summary>
        /// Do the action until timeout.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="T5">The type of the 5.</typeparam>
        /// <typeparam name="T6">The type of the 6.</typeparam>
        /// <typeparam name="T7">The type of the 7.</typeparam>
        /// <typeparam name="T8">The type of the 8.</typeparam>
        /// <typeparam name="T9">The type of the 9.</typeparam>
        /// <typeparam name="T10">The type of the 10.</typeparam>
        /// <typeparam name="T11">The type of the 11.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public bool DoIt<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action, object[] parameters, TimeSpan timeout)
        {
            return this.DoItImp(action, parameters, timeout);
        }

        /// <summary>
        /// Do the action until timeout.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="T5">The type of the 5.</typeparam>
        /// <typeparam name="T6">The type of the 6.</typeparam>
        /// <typeparam name="T7">The type of the 7.</typeparam>
        /// <typeparam name="T8">The type of the 8.</typeparam>
        /// <typeparam name="T9">The type of the 9.</typeparam>
        /// <typeparam name="T10">The type of the 10.</typeparam>
        /// <typeparam name="T11">The type of the 11.</typeparam>
        /// <typeparam name="T12">The type of the 12.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public bool DoIt<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action, object[] parameters, TimeSpan timeout)
        {
            return this.DoItImp(action, parameters, timeout);
        }

        /// <summary>
        /// Do the action until timeout.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="T5">The type of the 5.</typeparam>
        /// <typeparam name="T6">The type of the 6.</typeparam>
        /// <typeparam name="T7">The type of the 7.</typeparam>
        /// <typeparam name="T8">The type of the 8.</typeparam>
        /// <typeparam name="T9">The type of the 9.</typeparam>
        /// <typeparam name="T10">The type of the 10.</typeparam>
        /// <typeparam name="T11">The type of the 11.</typeparam>
        /// <typeparam name="T12">The type of the 12.</typeparam>
        /// <typeparam name="T13">The type of the 13.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public bool DoIt<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action, object[] parameters, TimeSpan timeout)
        {
            return this.DoItImp(action, parameters, timeout);
        }

        /// <summary>
        /// Do the action until timeout.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="T5">The type of the 5.</typeparam>
        /// <typeparam name="T6">The type of the 6.</typeparam>
        /// <typeparam name="T7">The type of the 7.</typeparam>
        /// <typeparam name="T8">The type of the 8.</typeparam>
        /// <typeparam name="T9">The type of the 9.</typeparam>
        /// <typeparam name="T10">The type of the 10.</typeparam>
        /// <typeparam name="T11">The type of the 11.</typeparam>
        /// <typeparam name="T12">The type of the 12.</typeparam>
        /// <typeparam name="T13">The type of the 13.</typeparam>
        /// <typeparam name="T14">The type of the 14.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public bool DoIt<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action, object[] parameters, TimeSpan timeout)
        {
            return this.DoItImp(action, parameters, timeout);
        }

        /// <summary>
        /// Do the action until timeout.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="T5">The type of the 5.</typeparam>
        /// <typeparam name="T6">The type of the 6.</typeparam>
        /// <typeparam name="T7">The type of the 7.</typeparam>
        /// <typeparam name="T8">The type of the 8.</typeparam>
        /// <typeparam name="T9">The type of the 9.</typeparam>
        /// <typeparam name="T10">The type of the 10.</typeparam>
        /// <typeparam name="T11">The type of the 11.</typeparam>
        /// <typeparam name="T12">The type of the 12.</typeparam>
        /// <typeparam name="T13">The type of the 13.</typeparam>
        /// <typeparam name="T14">The type of the 14.</typeparam>
        /// <typeparam name="T15">The type of the 15.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public bool DoIt<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action, object[] parameters, TimeSpan timeout)
        {
            return this.DoItImp(action, parameters, timeout);
        }

        /// <summary>
        /// Do the action until timeout.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <typeparam name="T5">The type of the 5.</typeparam>
        /// <typeparam name="T6">The type of the 6.</typeparam>
        /// <typeparam name="T7">The type of the 7.</typeparam>
        /// <typeparam name="T8">The type of the 8.</typeparam>
        /// <typeparam name="T9">The type of the 9.</typeparam>
        /// <typeparam name="T10">The type of the 10.</typeparam>
        /// <typeparam name="T11">The type of the 11.</typeparam>
        /// <typeparam name="T12">The type of the 12.</typeparam>
        /// <typeparam name="T13">The type of the 13.</typeparam>
        /// <typeparam name="T14">The type of the 14.</typeparam>
        /// <typeparam name="T15">The type of the 15.</typeparam>
        /// <typeparam name="T16">The type of the 16.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public bool DoIt<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action, object[] parameters, TimeSpan timeout)
        {
            return this.DoItImp(action, parameters, timeout);
        }
        #endregion

        /// <summary>
        /// DoIt method implementation
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        private bool DoItImp(Delegate action, object[] parameters, TimeSpan timeout) {
            var w = new Worker(action, parameters, this.evnt);
            var t = new Thread(new ThreadStart(w.Run));

            evnt.Reset();
            t.Start();

            if (evnt.WaitOne(timeout, false))
            {
                return true;
            }
            else
            {
                t.Abort();
                return false;
            }
        }

        #region Classe Worker
        private class Worker
        {
            private AutoResetEvent evnt;
            private Object[] parameters;
            private Delegate action;

            /// <summary>
            /// Initializes a new instance of the <see cref="Worker"/> class.
            /// </summary>
            /// <param name="action">The action.</param>
            /// <param name="parameters">The parameters.</param>
            /// <param name="evnt">The evnt.</param>
            public Worker(Delegate action, object[] parameters, AutoResetEvent evnt)
            {
                this.action = action;
                this.parameters = parameters;
                this.evnt = evnt;
            }

            /// <summary>
            /// Runs this instance.
            /// </summary>
            public void Run()
            {
                this.action.DynamicInvoke(parameters);
                evnt.Set();
            }
        }
        #endregion


    }
}
