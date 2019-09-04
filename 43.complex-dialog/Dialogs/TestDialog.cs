using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

namespace Microsoft.BotBuilderSamples
{
    class TestDialog : ComponentDialog
    {
        public TestDialog()
            : base(nameof(TopLevelDialog))
        {
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new NumberPrompt<int>(nameof(NumberPrompt<int>)));

            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {
                WidgetStep,
                JobTitleStep,
                CompanyNameStep,
            }));

            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> WidgetStep(WaterfallStepContext sc, CancellationToken cancellationToken)
        {

            var promptOptions = new PromptOptions { Prompt = MessageFactory.Text("How many widgets did you sell last month?") };

            return await sc.PromptAsync(nameof(TextPrompt), promptOptions);
        }

        private async Task<DialogTurnResult> JobTitleStep(WaterfallStepContext sc, CancellationToken cancellationToken)
        {

            var promptOptions = new PromptOptions { Prompt = MessageFactory.Text("What is your job title?") };

            return await sc.PromptAsync(nameof(TextPrompt), promptOptions);
        }

        private async Task<DialogTurnResult> CompanyNameStep(WaterfallStepContext sc, CancellationToken cancellationToken)
        {

            var promptOptions = new PromptOptions { Prompt = MessageFactory.Text("What is the name of your company?") };

            return await sc.PromptAsync(nameof(TextPrompt), promptOptions);
        }
    }
}
