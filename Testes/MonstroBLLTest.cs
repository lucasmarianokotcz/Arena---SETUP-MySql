﻿using Intermediario;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Monstro;
using System.Collections.Generic;

namespace Testes
{
    [TestClass]
    public class MonstroBLLTest
    {
        [TestMethod]
        public void Select()
        {
            List<Monstro> lstMonstros = new MonstroBLL().Select();
            Assert.IsTrue(lstMonstros != null, "Deve acessar o banco e retornar uma lista de monstros (mesmo que vazia).");
        }

        [TestMethod]
        public void GerarMonstroAleatorio()
        {
            var monstro = new MonstroBLL().GerarMonstroAleatorio();
            Assert.IsTrue(monstro != null, "Deve retornar um monstro aleatório.");
        }
    }
}
