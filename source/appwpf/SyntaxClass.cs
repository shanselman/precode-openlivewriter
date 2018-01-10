/************************************************************************************  
' Copyright (C) 2009 Anthony Bouch (http://www.58bits.com) under the terms of the
' Microsoft Public License (Ms-PL http://www.codeplex.com/precode/license)
'***********************************************************************************/
using System;
using System.Collections;

namespace FiftyEightBits.PreCode
{
    public class SyntaxClass : IComparable<SyntaxClass>, IComparable
    {
        public SyntaxClass(string label, string attribute)
        {
            ClassLabel = label;
            ClassAttribute = attribute;
        }

        private string _classAttribute = string.Empty;
        public string ClassAttribute
        {
            get { return _classAttribute; }
            set { _classAttribute = value; }
        }

        private string _classLabel = string.Empty;
        public string ClassLabel
        {
            get { return _classLabel; }
            set { _classLabel = value; }
        }

        public override string ToString()
        {
            return ClassLabel;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is SyntaxClass))
                return false;

            return ClassAttribute.Equals(((SyntaxClass)obj).ClassAttribute, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return ClassAttribute.GetHashCode();
        }


        #region IComparable<CodeClass> Members

        public int CompareTo(SyntaxClass other)
        {
            return ClassAttribute.CompareTo(other.ClassAttribute);
        }

        #endregion

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj is SyntaxClass)
                return CompareTo(obj as SyntaxClass);
            
            throw new ArgumentException("CompareTo expects type of SyntaxClass");
        }

        #endregion

    }

    public class SyntaxClassCollection : CollectionBase
    {

        public SyntaxClass this[int index]
        {
            get
            {
                return ((SyntaxClass)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }

        public SyntaxClass this[string classAttribute]
        {
            get
            {
                foreach (SyntaxClass item in List)
                {
                    if (item.ClassAttribute.Equals(classAttribute, StringComparison.Ordinal))
                        return item;
                }
                return List[0] as SyntaxClass;
            }
        }

        public int Add(SyntaxClass item)
        {
            return (List.Add(item));
        }

        public int IndexOf(SyntaxClass item)
        {
            return (List.IndexOf(item));
        }

        public void Insert(int index, SyntaxClass item)
        {
            List.Insert(index, item);
        }

        public void Remove(SyntaxClass item)
        {
            List.Remove(item);
        }

        public bool Contains(SyntaxClass item)
        {
            return (List.Contains(item));
        }
    }
}
