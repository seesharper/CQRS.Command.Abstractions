using System;

namespace CQRS.Command.Abstractions
{
    /// <summary>
    /// Represents a command that can have a result.
    /// Command handlers are expected to set the result of the command.
    /// </summary>
    /// <typeparam name="TResult">The type of result set by the handler.</typeparam>
    public abstract record Command<TResult>
    {
        private TResult _result;

        private bool _resultHaveBeenSet;

        /// <summary>
        /// Gets the result of the command.
        /// </summary>        
        public TResult GetResult()
        {
            if (!_resultHaveBeenSet)
            {
                throw new InvalidOperationException($"The result of the command({this.GetType()}) has not been set. Make sure to call {nameof(SetResult)} on the command in the command handler.");
            }

            return _result;
        }

        /// <summary>
        /// Sets the result of the command.
        /// </summary>
        /// <param name="result">The result value.</param>
        public void SetResult(TResult result)
        {
            _result = result;
            _resultHaveBeenSet = true;
        }

        /// <summary>
        /// Returns true if the result of the command has been set.
        /// </summary>
        /// <returns>true if the result has been set, otherwise false.</returns>
        public bool HasResult() => _resultHaveBeenSet;
    }
}