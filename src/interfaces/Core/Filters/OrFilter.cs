// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org
// ****************************************************************
using System;
using System.Collections;

namespace NUnit.Core.Filters
{
	/// <summary>
	/// Combines multiple filters so that a test must pass one 
	/// of them in order to pass this filter.
	/// </summary>
	[Serializable]
	public class OrFilter : TestFilter
	{
		private ArrayList filters = new ArrayList();

		/// <summary>
		/// Constructs an empty OrFilter
		/// </summary>
		public OrFilter() { }

		/// <summary>
		/// Constructs an AndFilter from an array of filters
		/// </summary>
		/// <param name="filters"></param>
		public OrFilter( params TestFilter[] filters )
		{
			this.filters.AddRange( filters );
		}

		/// <summary>
		/// Adds a filter to the list of filters
		/// </summary>
		/// <param name="filter">The filter to be added</param>
		public void Add( TestFilter filter )
		{
			this.filters.Add( filter );
		}

		/// <summary>
		/// Return an array of the composing filters
		/// </summary>
		public TestFilter[] Filters
		{
			get
			{
				return (TestFilter[])filters.ToArray(typeof(TestFilter));
			}
		}

		/// <summary>
		/// Checks whether the OrFilter is matched by a test
		/// </summary>
		/// <param name="test">The test to be matched</param>
		/// <returns>True if any of the component filters pass, otherwise false</returns>
		public override bool Pass( ITest test )
		{
			foreach( TestFilter filter in filters )
				if ( filter.Pass( test ) )
					return true;

			return false;
		}

		/// <summary>
		/// Checks whether the OrFilter is matched by a test
		/// </summary>
		/// <param name="test">The test to be matched</param>
		/// <returns>True if any of the component filters match, otherwise false</returns>
		public override bool Match( ITest test )
		{
			foreach( TestFilter filter in filters )
				if ( filter.Match( test ) )
					return true;

			return false;
		}
	}
}
