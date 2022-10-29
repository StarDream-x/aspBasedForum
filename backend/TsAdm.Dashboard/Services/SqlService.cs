using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TsAdm.Dashboard.Services
{
    public class SqlService
    {
        List<Todos> todos = new List<Todos>();

        public SqlService()
        {
            Todos todo = new Todos();
            todo.id = 1;
            todo.work = "AAA";
            todo.complete = false;
            todos.Add(todo);
            todo = new Todos();
            todo.id = 2;
            todo.work = "BBB";
            todo.complete = false;
            todos.Add(todo);
        }

        public List<Todos> getTodos()
        {
            return todos;
        }

        public Todos getTodos(int id)
        {
            try
            {
                foreach(Todos todo in todos)
                {
                    if (todo.id == id) return todo;
                }
                throw new Exception("no such id");
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return null;
            }
            
        }

        public Todos addTodos(Todos todo)
        {
            try
            {
                foreach (Todos old in todos) if (old.id == todo.id)
                    {
                        throw new Exception("id重复");
                    }
                todos.Add(todo);
                return todo;
            }catch(Exception err)
            {
                Console.WriteLine(err.Message);
                return null;
            }
        }

        public void deleteTodos(int id)
        {
            try
            {
                int l = todos.Count;
                for(int i=0;i<l;++i) if (todos[i].id == id)
                    {
                        todos.Remove(todos[i]);
                        return;
                    }
                throw new Exception("no such id");
            }catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }
            
        }

        public void update(Todos todo)
        {
            try
            {
                foreach (Todos old in todos)
                {
                    if (todo.id == old.id)
                    {
                        todos.Remove(old);
                        todos.Add(todo);
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }
    }
}