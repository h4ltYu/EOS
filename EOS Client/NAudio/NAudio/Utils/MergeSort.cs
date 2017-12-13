using System;
using System.Collections.Generic;

namespace NAudio.Utils
{
	// Token: 0x0200011A RID: 282
	internal class MergeSort
	{
		/// <summary>
		/// In-place and stable implementation of MergeSort
		/// </summary>
		// Token: 0x06000645 RID: 1605 RVA: 0x0001444C File Offset: 0x0001264C
		private static void Sort<T>(IList<T> list, int lowIndex, int highIndex, IComparer<T> comparer)
		{
			if (lowIndex >= highIndex)
			{
				return;
			}
			int num = (lowIndex + highIndex) / 2;
			MergeSort.Sort<T>(list, lowIndex, num, comparer);
			MergeSort.Sort<T>(list, num + 1, highIndex, comparer);
			int num2 = num;
			int num3 = num + 1;
			while (lowIndex <= num2 && num3 <= highIndex)
			{
				if (comparer.Compare(list[lowIndex], list[num3]) <= 0)
				{
					lowIndex++;
				}
				else
				{
					T value = list[num3];
					for (int i = num3 - 1; i >= lowIndex; i--)
					{
						list[i + 1] = list[i];
					}
					list[lowIndex] = value;
					lowIndex++;
					num2++;
					num3++;
				}
			}
		}

		/// <summary>
		/// MergeSort a list of comparable items
		/// </summary>
		// Token: 0x06000646 RID: 1606 RVA: 0x000144E7 File Offset: 0x000126E7
		public static void Sort<T>(IList<T> list) where T : IComparable<T>
		{
			MergeSort.Sort<T>(list, 0, list.Count - 1, Comparer<T>.Default);
		}

		/// <summary>
		/// MergeSort a list 
		/// </summary>
		// Token: 0x06000647 RID: 1607 RVA: 0x000144FD File Offset: 0x000126FD
		public static void Sort<T>(IList<T> list, IComparer<T> comparer)
		{
			MergeSort.Sort<T>(list, 0, list.Count - 1, comparer);
		}
	}
}
