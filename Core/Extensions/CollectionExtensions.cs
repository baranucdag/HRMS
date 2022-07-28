using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Core.Extensions
{
    public static class CollectionExtensions
    {
        // Get - getir
        public static V Get<K, V>(this ConcurrentDictionary<K, V> dictionary, K key)
        {
            if (dictionary.ContainsKey(key))
                return dictionary[key];
            else
                return default(V);
        }

        // Gets - hangisi varsa getir
        public static V Gets<K, V>(this ConcurrentDictionary<K, V> dictionary, params K[] key)
        {
            foreach (var item in key)
            {
                if (dictionary.ContainsKey(item))
                    return dictionary[item];
            }
            return default(V);
        }

        // TrimArray - string arrayin tüm elemanlarını trimler
        public static string[] TrimArray(this string[] array)
        {
            if (array == null) return null;

            for (int i = 0; i <= array.Length - 1; i++)
            {
                array[i] = array[i].Trim();
            }

            return array;
        }

        // TrimList - listeyi trimler
        public static List<string> TrimList(this List<string> list)
        {
            if (list != null)
            {
                for (int i = 0; i <= list.Count - 1; i++)
                {
                    list[i] = list[i].Trim();
                }
                return list;
            }
            else
                return new List<string>();
        }


        // ToGenericType - datarowu generic typea çevirir
        private static T ToGenericType<T>(DataRow dr)
        {
            Type type = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in type.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        var o = dr[column.ColumnName];
                        if (o.GetType() != typeof(DBNull))
                            pro.SetValue(obj, o, null);

                    }
                }
            }

            return obj;
        }

        // ToList - datatableyi generic liste çevirir
        public static List<T> ToList<T>(this DataTable dt)
        {
            List<T> list = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                T t = ToGenericType<T>(row);
                list.Add(t);
            }

            return list;
        }

        // ToStr - listeyi metne çevirir
        public static string ToStr(this List<string> list)
        {
            string res = string.Empty;

            if (list != null)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var item in list)
                {
                    sb.AppendLine(item);
                }

                res = sb.ToString();
            }

            return res;
        }

        // AddIfNotExists - listede yoksa ekle
        public static void AddIfNotExists(this IList<string> list, string str)
        {
            if (!list.Contains(str)) list.Add(str);
        }

        // AddIfNotExists - listede yoksa ekle
        public static void AddIfNotExists<T>(this IList<T> list, T str)
        {
            if (!list.Contains(str)) list.Add(str);
        }



        // ToPagination - IQuerable olan listeyi dbpagination olarak çeker
        public static IQueryable<T> ToPagination<T>(this IQueryable<T> list, int pageSize, int page)
        {
            return list.AsQueryable<T>().Skip((page - 1) * pageSize).Take(pageSize);
        }

        // ToPaginationFilter - IQuerable olan listeyi filtrelemeye yarar.
        /*public static IQueryable<T> ToPaginationFilter<T>(this IQueryable<T> list, string key, string value, string matchMode)
        {
            var parameter = Expression.Parameter(typeof(T));
            var field = Expression.PropertyOrField(parameter, key);

            var props = typeof(T).GetProperties();
            var oProp = props.FirstOrDefault(x => x.Name.ToLowerEng() == key.ToLowerEng());

            if (oProp != null)
            {

                if (oProp.PropertyType == typeof(string))
                {
                    // case insensitive desteki aramak için yapıldı
                    var target = Expression.Constant(value.ToUpperEng());
                    var target2 = Expression.Call(target, "ToUpper", null);
                    var contains = Expression.Call(field, matchMode, null, target2);
                    var lambda = Expression.Lambda<Func<T, bool>>(contains, parameter);
                    return list.Where(lambda).AsQueryable<T>();
                }
                else if (oProp.PropertyType == typeof(Guid))
                {
                    var target = Expression.Constant(value.ToUpperEng());
                    var target2 = Expression.Call(target, "ToUpper", null);
                    var contains = Expression.Call(field, "Equals", null, target2);
                    var lambda = Expression.Lambda<Func<T, bool>>(contains, parameter);
                    list.ToList();
                    return list.Where(lambda).AsQueryable<T>();
                }
                else if (oProp.PropertyType == typeof(int))
                {
                    var target = Expression.Constant(value.ToIntDef(-1));
                    var contains = Expression.Call(field, "Equals", null, target);
                    var lambda = Expression.Lambda<Func<T, bool>>(contains, parameter);
                    list.ToList();
                    return list.Where(lambda).AsQueryable<T>();
                }
                else if (oProp.PropertyType == typeof(Nullable<int>))
                {
                    var target = Expression.Constant(value.ToIntDef(-1).ToString());
                    var target2 = Expression.Call(target, "ToUpper", null);
                    var contains = Expression.Call(field, "Equals", null, target2);
                    var lambda = Expression.Lambda<Func<T, bool>>(contains, parameter);
                    list.ToList();
                    return list.Where(lambda).AsQueryable<T>();
                }
                // todo: datetime
                // todo: bool
                else
                {
                    var target = Expression.Constant(Convert.ToInt32(value));
                    var contains = Expression.Call(field, matchMode, null, target);
                    var lambda = Expression.Lambda<Func<T, bool>>(contains, parameter);
                    return list.Where(lambda).AsQueryable<T>();
                }
            }

            return list;
        }*/

        // ToApplyOrder- IQuerable olan listeyi sort'lamaya yarar. // orderby = thenby, thenbydescending, orderby, orderbydescending 
        public static IOrderedQueryable<T> ToApplyOrder<T>(this IQueryable<T> source, string property, string methodName)
        {
            // todo: bu kısım ArkLicense.Common projesine alınacak -Oğuzhan
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }

        // Move - Liste üzerindeki elemanı başka sıraya taşır
        public static void Move<T>(this List<T> list, int oldIndex, int newIndex)
        {
            var item = list[oldIndex];

            list.RemoveAt(oldIndex);

            if (newIndex > oldIndex) newIndex--;

            list.Insert(newIndex, item);
        }

        // Move - Liste üzerindeki elemanı başka sıraya taşır
        public static void Move<T>(this List<T> list, T item, int newIndex)
        {
            if (item != null)
            {
                var oldIndex = list.IndexOf(item);
                if (oldIndex > -1)
                {
                    list.RemoveAt(oldIndex);

                    if (newIndex > oldIndex) newIndex--;

                    list.Insert(newIndex, item);
                }
            }
        }

        // ToDataTable - listeyi datatableye dönüştür
        public static DataTable ToDataTable<T>(this IList<T> data, bool datesToLocalTime = false)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in data)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    if (prop.PropertyType == typeof(DateTime))
                    {
                        if (prop.GetValue(item) == null)
                        {
                            row[prop.Name] = DBNull.Value;
                        }
                        else if (datesToLocalTime)
                        {
                            row[prop.Name] = ((DateTime)prop.GetValue(item)).ToLocalTime();
                        }
                        else
                        {
                            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                        }
                    }
                    else
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }

            return table;
        }
    }
}
