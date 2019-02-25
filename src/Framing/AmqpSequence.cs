﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Azure.Amqp.Framing
{
    using System.Collections;
    using System.Collections.Generic;
    using Microsoft.Azure.Amqp.Encoding;

    /// <summary>
    /// Defines the AMQP sequence body type of a message.
    /// </summary>
    public sealed class AmqpSequence : DescribedList
    {
        /// <summary>
        /// The descriptor name.
        /// </summary>
        public const string Name = "amqp:amqp-sequence:list";
        /// <summary>
        /// The descriptor code.
        /// </summary>
        public const ulong Code = 0x0000000000000076;

        IList innerList;

        /// <summary>
        /// Initializes the sequence.
        /// </summary>
        public AmqpSequence()
            : this(new List<object>()) 
        {
        }

        /// <summary>
        /// Initializes the sequence from a list.
        /// </summary>
        /// <param name="innerList">The list containing objects in the sequence.</param>
        public AmqpSequence(IList innerList)
            : base(Name, Code)
        {
            this.innerList = innerList;
        }

        /// <summary>
        /// Gets the list containing the objects in the sequence.
        /// </summary>
        public IList List
        {
            get { return this.innerList; }
        }

        internal override int FieldCount
        {
            get { return this.innerList.Count; }
        }

        /// <summary>
        /// Returns a string that represents the object.
        /// </summary>
        /// <returns>A string representing the object.</returns>
        public override string ToString()
        {
            return "sequence()";
        }

        internal override int OnValueSize()
        {
            return ListEncoding.GetValueSize(this.innerList);
        }

        internal override void OnEncode(ByteBuffer buffer)
        {
            foreach (object item in this.innerList)
            {
                AmqpEncoding.EncodeObject(item, buffer);
            }
        }

        internal override void OnDecode(ByteBuffer buffer, int count)
        {
            for (int i = 0; i < count; i++)
            {
                this.innerList.Add(AmqpEncoding.DecodeObject(buffer));
            }
        }
    }
}
