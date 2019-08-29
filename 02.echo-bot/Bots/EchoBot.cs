// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace Microsoft.BotBuilderSamples.Bots
{
    public class EchoBot : ActivityHandler
    {
        private BotState _userState;

        public EchoBot(UserState userState) {
            _userState = userState;
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var counterStateAccessor = _userState.CreateProperty<CounterState>(nameof(CounterState));
            var counter = await counterStateAccessor.GetAsync(turnContext, () => new CounterState());
            await turnContext.SendActivityAsync(MessageFactory.Text($"{counter.count}: {turnContext.Activity.Text}"), cancellationToken);
            counter.count += 1;

            await _userState.SaveChangesAsync(turnContext);
        }


        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Hello and welcome!"), cancellationToken);
                }
            }
        }
    }
}
