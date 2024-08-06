using CarAuction.Core.Models;
using CarAuction.Core.Repositories.Comments;
using CarAuction.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Data.Repositories.Comments
{
    public class CommentWriteRepository : WriteRepository<Comment>, ICommentWriteRepository
    {
        public CommentWriteRepository(AuctionDbContext context) : base(context)
        {
        }
    }
}
