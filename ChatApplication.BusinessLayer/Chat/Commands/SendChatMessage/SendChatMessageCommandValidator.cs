using FluentValidation;

namespace ChatApplication.Services.Chat.Commands.SendChatMessage
{
    public class SendChatMessageCommandValidator: AbstractValidator<SendChatMessageCommand>
    {
        public SendChatMessageCommandValidator()
        {
            RuleFor(u => u.ChatId)
                .NotEmpty();
            RuleFor(u => u.Message)
                .NotEmpty();
        }
    }
}