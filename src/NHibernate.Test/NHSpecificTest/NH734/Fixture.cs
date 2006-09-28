using System.Collections;
using System.Collections.Generic;
using System.Xml;
using NHibernate.Cfg;
using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.NH734
{
	[TestFixture]
	public class Fixture : BugTestCase
	{
		public override string BugNumber
		{
			get
			{
				return "NH734";
			}
		}

		[TestAttribute]
		public void LimitProblem()
		{
			using (ISession session = sessions.OpenSession())
			{

				ICriteria criteria = session.CreateCriteria(typeof(MyClass));
				criteria.SetMaxResults(100);
				criteria.SetFirstResult(0);
				try
				{
					session.BeginTransaction();
					IList<MyClass> result = criteria.List<MyClass>();
					session.Transaction.Commit();

				}
				catch
				{
					if (session.Transaction != null)
					{
						session.Transaction.Rollback();
					}
					throw;
				}
			}
		}
	}
}
