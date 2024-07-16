using FluentValidation;

namespace ChatApplication.Services.Chat.Commands.UpdateSendMessagesChat;

public class UpdateSendMessagesChatCommandValidator: AbstractValidator<UpdateSendMessagesChatCommand>
{
    public UpdateSendMessagesChatCommandValidator()
    {
        RuleFor(u => u.Id)
            .NotEmpty();
        RuleFor(u => u.SendMessages)
            .NotEmpty();
    }
}