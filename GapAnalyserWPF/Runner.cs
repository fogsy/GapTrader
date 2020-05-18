﻿using Foundations.Interfaces;
using GapAnalyser;
using GapAnalyser.Interfaces;
using System;
using System.Windows;
using System.Windows.Media;

namespace GapAnalyserWPF
{
    internal sealed class Runner : IRunner
    {
        public Runner(IContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Run(object sender, IRunnable runnable)
        {
            _context.Send(_ =>
            {
                var owner = GetOwner(sender);
                var window = new GenericWindow { DataContext = runnable, Owner = owner };
                window.ShowDialog();
            });
        }

        public void RunConcurrently(object sender, IRunnable runnable)
        {
            _context.Send(_ =>
            {
                var owner = GetOwner(sender);
                var window = new GenericWindow { DataContext = runnable, Owner = owner };
                window.Show();
            });
        }

        public void Run(object sender, Message message)
        {
            _context.Send(_ =>
            {
                var owner = GetOwner(sender);
                MessageBox.Show(owner, message.ContentKey, message.NameKey, MessageBoxButton.OK, MessageBoxImage.Exclamation);
            });
        }

        public bool RunForResult(object sender, Message message)
        {
            var result = false;

            _context.Send(_ =>
            {
                var owner = GetOwner(sender);

                result = MessageBox.Show(owner, message.ContentKey, message.NameKey,
                   MessageBoxButton.YesNo,
                   MessageBoxImage.Information) == MessageBoxResult.Yes;
            });

            return result;
        }

        /// <summary>
        /// Searches the visual tree for an element whose data context is the sender, and
        /// returns the containing window. If no such element is found, then returns the 
        /// first window in the application's collection of windows. If there are no windows,
        /// returns null.
        /// </summary>
        private static Window? GetOwner(object sender)
        {
            bool HasDataContext(DependencyObject d) => d is FrameworkElement element &&
                                                       element.DataContext == sender;

            bool SenderIsInTree(DependencyObject parent)
            {
                if (HasDataContext(parent))
                {
                    return true;
                }

                var childrenCount = VisualTreeHelper.GetChildrenCount(parent);

                for (var index = 0; index < childrenCount; ++index)
                {
                    var child = VisualTreeHelper.GetChild(parent, index);

                    if (HasDataContext(child) ||
                        SenderIsInTree(child))
                    {
                        return true;
                    }
                }

                return false;
            }

            Window? SearchWindows()
            {
                foreach (var o in Application.Current.Windows)
                {
                    if (o is MainWindow root && SenderIsInTree(root))
                    {
                        return root;
                    }
                }

                return Application.Current.Windows.Count > 0
                   ? Application.Current.Windows[0]
                   : null;
            }

            var owner = SearchWindows();
            return owner;
        }

        private readonly IContext _context;
    }
}