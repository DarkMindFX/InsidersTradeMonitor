﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ITM.Interfaces
{
    public interface IDalBase<TEntity> : IInitializable
    {
        IList<TEntity> GetAll();

        TEntity Insert(TEntity entity);

        TEntity Update(TEntity entity);
    }
}
