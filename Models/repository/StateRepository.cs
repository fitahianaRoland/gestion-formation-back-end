using GestionFormation.Models.classes;
using Microsoft.EntityFrameworkCore;

namespace GestionFormation.Models.repository
{
    public class StateRepository
    {
        
            private readonly ApplicationDbContext _context;

            public StateRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<State>> FindAll()
            {
                return await _context.states.ToListAsync() ?? new List<State>();
            }

            public async Task<State?> FindById(int id)
            {
                return await _context.states.FindAsync(id);
            }

            public async Task Add(State state)
            {
                if (state == null)
                {
                    throw new ArgumentNullException(nameof(state));
                }

                await _context.states.AddAsync(state);
                await _context.SaveChangesAsync();
            }

            public async Task<State?> Update(int id, State state)
            {
                var existingState = await FindById(id);
                if (existingState == null)
                {
                    return null;
                }

                existingState.Value = state.Value;
                existingState.Description = state.Description;
                
                await _context.SaveChangesAsync();
                
                return existingState;
            }

      
            public async Task<bool> Delete(int id)
            {
                var stateToDelete = await FindById(id);
                    if (stateToDelete == null)
                    {
                        return false;
                    }

                    _context.states.Remove(stateToDelete);
                    await _context.SaveChangesAsync();
                    return true;
            }

    }
}



