using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace FunctionalProgramming
{
    // 部分应用和参数重用： 柯里化允许您将一个多参数函数转换为一系列单参数函数。这使得您可以在一次调用中提供部分参数，并在以后的调用中重用这些参数。这有助于简化函数调用，并在代码中更方便地复用逻辑。

    // 高阶函数的创建： 柯里化可以用来创建高阶函数，即接受或返回函数的函数。这有助于实现通用的操作，如映射、过滤、排序等，从而提高代码的抽象程度和可重用性。

    // 定制化函数： 柯里化允许您为特定的场景创建函数，通过预设一些参数值，从而简化函数调用，并将其定制为特定用途。

    // 函数组合： 柯里化使得函数的组合变得更加直观和可读。您可以将多个柯里化的函数组合在一起，从而实现更复杂的逻辑。

    // 事件处理和回调： 在事件处理和回调场景中，柯里化可以帮助您在创建事件处理函数时预设一些参数，以简化回调的创建和维护。

    // 表达式树： 在涉及 LINQ 查询或动态表达式树的情况下，柯里化可以帮助您创建更具动态性和可定制性的查询。

    // 函数式编程范式： 对于采用函数式编程范式的项目，柯里化可以提供更符合函数式编程思维的代码结构。

    // 延迟计算和惰性求值： 柯里化可以用于创建具有惰性求值特性的函数，从而在需要时才进行计算，提高性能和效率。

    // 请注意，尽管柯里化在C#中有这些应用，但它并不是在所有情况下都是最佳选择。有些情况下，简单的方法参数可能更适合，取决于项目的需求和团队的偏好。柯里化的优势在于它能够增强代码的组合性和灵活性，但也需要在使用时进行权衡和谨慎考虑。
	static class CurryingExtensions
	{
        //在 Currying<T1, T2, TOutput> 函数中，每次调用都会返回一个闭包函数，而不是立即执行函数 f。
        //这个闭包函数会捕获参数 T1，然后返回一个 "接受参数 T2 的闭包函数"。
        //只有当这个 "接受参数 T2 的闭包函数（返回的函数）" 被调用时，才会实际调用 f 函数
        //，并且在闭包函数的上下文中，参数 T1 和传递的 T2 会被传递给 f。
		public static Func<T1, Func<T2, TOutput>>
			Currying<T1, T2, TOutput>(this Func<T1, T2, TOutput> f)
				=> x => y => f(x, y);

		public static Func<T1, Func<T2, Func<T3, TOutput>>>
			Currying<T1, T2, T3, TOutput>(this Func<T1, T2, T3, TOutput> f)
				=> x => y => z => f(x, y, z);
	}

	public class MainFunctionalProgramming : MonoBehaviour
	{
		private Button btnStart;
		private EasyEvent _easyEvent = new EasyEvent();
		private int counter;
		
		void Start()
		{
			btnStart.onClick.AddListener(_easyEvent.Trigger);
		}

		//柯里化测试
		void CurryingTest()
		{
			var happyWater = new Func<float, int, float>
					((float price, int number) => number * price)
				.Currying();
		
			var cocaHappyWater = happyWater(3.5f);
			var pepsiHappyWater = happyWater(3);
			var mcdHappyWater = happyWater(9);
		
			var calcPrice = new Func<Func<int, float>, float, int, float>
					((calc, discount, number) => discount * calc(number))
				.Currying();
		
			var pepsiPriceCalc = calcPrice(pepsiHappyWater);
			var cocaPriceCalc = calcPrice(cocaHappyWater);
		
			var priceCalcA = pepsiPriceCalc(1);
			var priceCalcB = cocaPriceCalc(0.8f);
		
			var priceA = priceCalcA(3);
			var priceB = priceCalcB(5);
			var total = priceA + priceB;
		
			print(total);
		}
	}
}

