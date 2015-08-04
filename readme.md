This is a modern, portable fork of Aaron Ericksons metalinq project, available on [CodePlex](https://metalinq.codeplex.com) 

He has a [blogpost introducing it](http://nomadic-developer.com/2007/05/24/announcing-metalinq-linq-to-expressions/), which I have paraphrased below:

Broadly speaking, the project allows you to serialize, modify, and deserialize LINQ Expressions.

-----


The ExpressionBuilder namespace allows you to create an Editable Shadow of an expression tree, modify it in place, and then by calling ToExpression on the shadow tree, generate a new, normal, immutable tree.  It has a class, EditableExpression, that has a factory method (CreateEditableExpression) that takes any expression, and returns an EditableExpression that mirrors the immutable Expression.

For example, to get the editable tree, you would do this to get an editable copy:

```
Expression immutable = someExpression; //you can’t change immutable directly
EditableExpression mutable = EditableExpression.CreateEditableExpression(immutable);
//..then do this to convert it back
Expression newCopy = mutable.ToExpression;
//pretend there are parens after ToExpression -  shortcoming in my blog software that does not allow me to say ToExpression with parens afterwards
```


In other words, you can now edit expression trees.  ExpressionBuilder is to Expressions what StringBuilder is to Strings.

I will warn you that you can easily shoot yourself in the foot with this.  As of the current version, you can easily create a cyclic graph, which, of course, will create an infinite loop when you try to convert it back into an immutable expression.  While I will be adding code to check for cycles in the future, there is no getting around that having full edit capability on the expression tree can cause subtle bugs.
