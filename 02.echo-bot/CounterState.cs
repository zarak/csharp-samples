    
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Microsoft.BotBuilderSamples
{
    // Stores User Welcome state for the conversation.
    // Stored in "Microsoft.Bot.Builder.ConversationState" and
    // backed by "Microsoft.Bot.Builder.MemoryStorage".

    public class CounterState
    {
        // Gets or sets whether the user has been welcomed in the conversation.
        public int count { get; set; } = 0;
    }
}
