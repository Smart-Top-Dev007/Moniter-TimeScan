   M S B A M L   `   `   `  ���� N  JMonitor.TaskControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Monitor.TaskControl.ViewMonitor.TaskControl.View  -    'Monitor.TaskControl.View.ExplorerWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     8local&clr-namespace:Monitor.TaskControl.View     -     � WindowStartupLocation$  CenterScreen=� � Topmost .  � Title$ Explorer Window��$	��800��$
��1320������  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ Black�� � Offset$ 0Q�� $ 	#FF133A5B��$	 0.5Q�� $ Black��$ 1Q�.��� $��5,5,5,5q� � ColumnDefinitions �� $
��140* ��� $	��80* �.+�� $��0�� � RowDefinitions �� $��3* ��� $	��70* �.+��� -   $��UnderBorder��$��
11,-1,15,3q�$��2q�
�������	 �� CornerRadius$	 2��� $��0��$��0,0,15,0q� �� $��* ��� $��* ��� $��* �.+�� $��0��.+��� -   $��chSmall��$��Center=�$��Center=�
 � LayoutTransform
 ��  �� ScaleX$	 1.5Q� �� ScaleY$	 1.5Q����  �� $	��20* ��� $	��30* �.+�� $��0��.+��� $
��Left=�$��-3, -3, 0, 0q�$	����$��Center=�$2��,pack://application:,,,/Resource/#FontAwesome)�
�������$��Center=�$��13(�� $��1��.+��� $��Small��
�������$��-3, -5, 0, -2q�$
��11px(�$
��Left=�$��Center=�� $��1��.+��� -   $��chMedium��$��Center=�$��Center=�
 �� $	 1.5Q�$	 1.5Q����  �� $	��20* ��� $	��30* �.+�� $��0��.+��� $
��Left=�$��-3, -3, 0, 0q�$	����$��Center=�$2��,pack://application:,,,/Resource/#FontAwesome)�
�������$��Center=�$��13(�� $��1��.+��� $��Medium��
�������$��-3, -5, 0, -2q�$
��11px(�$
��Left=�$��Center=�� $��2��.+��� -   $��chLarge��$��Center=�$��Center=�
 �� $	 1.5Q�$	 1.5Q����  �� $	��20* ��� $	��30* �.+�� $��0��.+��� $
��Left=�$��-3, -3, 0, 0q�$	����$��Center=�$2��,pack://application:,,,/Resource/#FontAwesome)�
�������$��Center=�$��13(�� $��1��.+��� $��Large��
�������$��-3, -5, 0, -2q�$
��11px(�$
��Left=�$��Center=��� -    ��Name$ 	ImageList��$��1��$��15, 15, 15, 15q�$��Disabled=�-  'System.Windows.Input.KeyboardNavigation   TabNavigation$ Continue=�$
��Auto=���.  ���� .R��  � Orientation$ 
Horizontal=�$	��Top=�����  �� 
TargetType" ��.�-   ��  �� Property �@ 	IsTabStop �� Value . ��  � ��$ 10q�� $��1��$��7, 0, 10, 0q� �� $	��20* ��� $	��25* ��� $	��20* �.+�� $��0��.+��� -   $��	backImage��$��130, 20, 130, 20q��� -	   $��
BackBorder��$��0��$��100, 25, 100, 25q�$��2,2,2,2q�
�������$	 4��� $��1��.+��� -
   $��currentImage��$��
2, 2, 3, 2q��� -   $��CurrentBorder��$��1��$��4,4,4,4q�
�������$	 4��� $��2��.+��� -   $��forwardImage��$��130, 20, 130, 20q��� -   $��ForwardBorder��$��2��$��100, 25, 100, 25q�$��1,1,1,1q�
�������$	 4��