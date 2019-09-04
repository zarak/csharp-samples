using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

namespace Microsoft.BotBuilderSamples
{
    class TestDialog : ComponentDialog
    {
        public TestDialog()
            : base(nameof(TestDialog))
        {
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new NumberPrompt<int>(nameof(NumberPrompt<int>)));

            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                WidgetStep,
                JobTitleStep,
                CompanyNameStep,
                ConfirmStep,
            }));

            InitialDialogId = nameof(WaterfallDialog);
        }

        private const string UserInfo = "value-userInfo";

        private async Task<DialogTurnResult> WidgetStep(WaterfallStepContext sc, CancellationToken cancellationToken)
        {
            Console.WriteLine(nameof(WidgetStep));

            var userProfile = (UserProfile)sc.Options;
            sc.Values[UserInfo] = userProfile;

            var promptOptions = new PromptOptions { Prompt = MessageFactory.Text("How many widgets did you sell last month?") };

            return await sc.PromptAsync(nameof(NumberPrompt<int>), promptOptions);
        }

        private async Task<DialogTurnResult> JobTitleStep(WaterfallStepContext sc, CancellationToken cancellationToken)
        {
            Console.WriteLine(nameof(JobTitleStep));

            var userProfile = (UserProfile)sc.Values[UserInfo];
            userProfile.WidgetSales = (int)sc.Result;

            var promptOptions = new PromptOptions { Prompt = MessageFactory.Text("What is your job title?") };

            return await sc.PromptAsync(nameof(TextPrompt), promptOptions, cancellationToken);
        }

        private async Task<DialogTurnResult> CompanyNameStep(WaterfallStepContext sc, CancellationToken cancellationToken)
        {
            Console.WriteLine(nameof(CompanyNameStep));
            var userProfile = (UserProfile)sc.Values[UserInfo];
            userProfile.JobTitle = (string)sc.Result;

            var promptOptions = new PromptOptions { Prompt = MessageFactory.Text("What is the name of your company?") };

            return await sc.PromptAsync(nameof(TextPrompt), promptOptions, cancellationToken);
        }

        private async Task<DialogTurnResult> ConfirmStep(WaterfallStepContext sc, CancellationToken cancellationToken)
        {

            Console.WriteLine(nameof(ConfirmStep));
            var userProfile = (UserProfile)sc.Values[UserInfo];
            userProfile.CompanyName = (string)sc.Result;

            return await sc.EndDialogAsync(userProfile, cancellationToken);
        }
    }
}
