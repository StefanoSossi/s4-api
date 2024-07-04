using s4.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Managers.Interfaces
{
    internal interface IClassesManager
    {
        Task<Class> CreateClass(CommentDto commentDto);

        Task<IEnumerable<CommentDto>> GetAllCommentsByPostId(Guid postId);

        Task<CommentDto> GetCommentByIdAndPostId(Guid postId, Guid id);

        Task<CommentDto> UpdateCommentByPostId(Guid postId, Guid id, Guid userId, CommentDto commentDto);

        Task<bool> DeleteByIdAndPostId(Guid postId, Guid id);
    }
}
