using Microsoft.EntityFrameworkCore;
using Operators.Server.Entities.Database;
using Operators.Server.Entities.Operators;
using Operators.Server.Entities.ViewModels;
using Operators.Server.Interfaces;

namespace Operators.Server.Services
{
	public class OperatorsRepositoryService : IOperatorsRepositoryService
	{
		private readonly ApplicationContext _context;

		public OperatorsRepositoryService(ApplicationContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Get operator by unique code
		/// </summary>
		/// <param name="code">Code operator</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns></returns>
		public async Task<Operator> GetOperator(long code, CancellationToken cancellationToken)
		{
			return await _context.Set<Operator>()
				.AsNoTracking()
				.FirstOrDefaultAsync(q => q.Code == code, cancellationToken);
		}

		/// <summary>
		/// Get operators
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>List of Operators</returns>
		public async Task<List<Operator>> GetOperators(CancellationToken cancellationToken)
		{
			var query = _context.Set<Operator>()
				.AsNoTracking();

			return await query.ToListAsync(cancellationToken);
		}

		/// <summary>
		/// Create Operator
		/// </summary>
		/// <param name="operatorViewModel">Info about of Operator</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Created Operator</returns>
		public async Task<Operator> CreateOperator(OperatorViewModelCreate operatorViewModel, CancellationToken cancellationToken)
		{
			var item = _context.Entry(new Operator() { Name = operatorViewModel.Name });

			item.State = EntityState.Added;

			await _context.SaveChangesAsync(cancellationToken);

			return item.Entity;
		}

		/// <summary>
		/// Update Operator by Code
		/// </summary>
		/// <param name="operatorViewModel">Info about of Operator</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Update Operator</returns>
		public async Task<Operator> UpdateOperator(OperatorViewModelUpdate operatorViewModel, CancellationToken cancellationToken)
		{
			var item = _context.Entry(new Operator() { Code = operatorViewModel.Code, Name = operatorViewModel.Name });

			item.State = EntityState.Modified;

			await _context.SaveChangesAsync(cancellationToken);

			return item.Entity;
		}

		/// <summary>
		/// Delete Operator by Code
		/// </summary>
		/// <param name="code">unique Code of Operator</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>True if Operator has been deleted</returns>
		public async Task<bool> DeleteOperator(long code, CancellationToken cancellationToken)
		{
			if (!await _context.Set<Operator>().AnyAsync(q => q.Code == code))
			{
				return false;
			}

			var item = _context.Entry(new Operator() { Code = code });

			item.State = EntityState.Deleted;

			await _context.SaveChangesAsync(cancellationToken);

			return true;
		}
	}
}
