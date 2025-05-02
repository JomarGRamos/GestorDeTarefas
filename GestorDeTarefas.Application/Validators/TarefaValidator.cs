using GestorDeTarefas.Domain.Entities;
using FluentValidation;
namespace GestorDeTarefas.Application.Validators
{
    public class TarefaValidator : AbstractValidator<Tarefa>
    {
        public TarefaValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("O título é obrigatório.")
                .MaximumLength(255).WithMessage("O título pode ter no máximo 255 caracteres.");

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("A descrição é obrigatória.")
                .MaximumLength(500).WithMessage("A descrição pode ter no máximo 500 caracteres.");

            RuleFor(x => x.DataVencimento)
                .NotNull().WithMessage("A data de vencimento é obrigatória.")
                .Must(data => data >= new DateTime(1754, 1, 1))
                .WithMessage("A data de vencimento deve ser maior ou igual a 01/01/1754.");

            RuleFor(x => (int)x.Status)
                .InclusiveBetween(0, 2).WithMessage("O status deve ser um valor entre 0 e 2.");
        }
    }
}