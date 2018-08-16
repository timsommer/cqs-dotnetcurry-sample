using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Cqs.SampleApp.Core.Cqs.Validation
{
    public class ValidationBag
    {
        public ValidationBag()
        {
            Messages = new List<ValidationMessage>();
        }

        public ValidationBag(IEnumerable<ValidationMessage> messages)
            : this()
        {
            if (messages == null) return;

            AddRange(messages);
        }

        public IList<ValidationMessage> Messages { get; set; }

        /// <summary>
        /// Add a message to the validation bag
        /// </summary>
        /// <param name="validationType"></param>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <returns>this validationbag in order to allow fluently use</returns>
        public ValidationBag Add(ValidationType validationType, string message, string code = "")
        {
            Messages.Add(new ValidationMessage(validationType, code, message));
            //for fluently uses
            return this;
        }

        public ValidationBag AddError(string message, string code = "")
        {
            return Add(ValidationType.Error, message, code);
        }

        public ValidationBag AddWarning(string message, string code = "")
        {
            return Add(ValidationType.Warning, message, code);
        }

        public ValidationBag AddNotification(string message, string code = "")
        {
            return Add(ValidationType.Notification, message, code);
        }

        /// <summary>
        /// Add a message to the validation bag
        /// </summary>
        /// <param name="validationMessage"></param>
        /// <returns></returns>
        public ValidationBag Add(ValidationMessage validationMessage)
        {
            if (validationMessage != null)
                Messages.Add(validationMessage);

            return this;
        }

        public bool IsValid => Messages.Count == 0;

        public void ThrowExceptionIfInvalid(bool throwIfWarnings = false)
        {
            var _throwException = Messages.Any(x => x.ValidationType == ValidationType.Error && (!throwIfWarnings || x.ValidationType == ValidationType.Warning));

            if (_throwException)
                throw new ValidationException(this);
        }

        public void Merge(ValidationBag validationBag)
        {
            AddRange(validationBag.Messages);
        }

        private void AddRange(IEnumerable<ValidationMessage> messages)
        {
            foreach (var _message in messages)
            {
                Messages.Add(_message);
            }
        }

        /// <summary>
        /// Checks if there are validation failures.
        /// </summary>
        /// <returns><c>true</c> if there are no Error messages, <c>false</c> otherwise.</returns>
        public bool ContainsErrors()
        {
            return Messages.Any(msg => msg.ValidationType == ValidationType.Error);
        }

        public bool ContainsWarnings()
        {
            return Messages.Any(msg => msg.ValidationType == ValidationType.Warning);
        }

        public string GetMessageString(ValidationType type)
        {
            return string.Join(Environment.NewLine, Messages.Where(x => x.ValidationType == type).Select(x => x.Message));
        }
    }
}