   M S B A M L   `   `   `  ���� N  JMonitor.TaskControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Monitor.TaskControl.ViewMonitor.TaskControl.View  K*clr-namespace:Monitor.TaskControl.ResourceMonitor.TaskControl.Resource  *    $Monitor.TaskControl.View.LoginWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     8local&clr-namespace:Monitor.TaskControl.View     ?controls*clr-namespace:Monitor.TaskControl.Resource     -   $	��285��$	��450��  � 
ResizeMode$  NoResize=�$��442.584��$	��250�� � WindowStartupLocation$ CenterScreen=�
���   �.��� ����  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ #eeeeee�� � Offset$ 0Q�� $ #c3c3c3��$ 1Q�� $ #eeeeee��$ 0.866Q� � RowDefinitions �� $��50 ��� .+�� -   $��50��$	��Top=����	#FF40A6D1 J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     
BlurRadius$ 10Q�	   ShadowDepth$	 3Q�
   	Direction$	
 270Q�   Opacity$	 0.2Q� � ColumnDefinitions �� $
��Auto ��� �� $
��Auto ��� $��60 �.+�3   -Monitor.TaskControl.Resource.HeaderDecoration  ]� $��2��$��0��$
��Left=�$��50����.  .+��� -    ��Name$ AVARTA��$��25��$��25�� K� ZIndex$ 1��$
��Left=�$��	14,0,0,10q�$��/Resource/net-on.png��$��Uniform=� J� RenderTransformOrigin$ 0.396,1.005&�$��Bottom=��� $�� Password of Server��$��Center=�$��Center=�
�������$��SemiBold"�$��21(� J� IsManipulationEnabled . $��0,5,5,5q�$	��0,6q��� $��2��$��2��$��Center=� �� Orientation$ 
Horizontal=�$��18,0,0,0q�$��50��.+��� -   $��39��$��40��$	����   CloseMinMaxButtonStyle#���   
������ 
������ 
�������$2��,pack://application:,,,/Resource/#FontAwesome)�$��20(�$��Bottom=�|�.  � $��0��$��1��$��Stretch=�$��Stretch=�$��0,40,0,0q�$ 0.499,0.602&� �� �� $	��228 ��� $��61.286 � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* �.+�� .+��� $��32,-10,50,31q�$��14(�$��Directory :���� -    ��Name$ WorkDirectory��$��Center=�$��105,-10,-224,31q�$��33��  SelectFileTextBoxStyle#���  ���	#FFFFFFFF���	#FFABADB3$��1q�$��14(�
���   � � ToolTip$ Work Directory��$��1��{��� *IsAvailable+�� $)��#Please select your work directory. ��
�������  LinkLabelTextBlockStyle#���  $	��227��$��12(�$��118,27,-190,10q��� -   $��Right=�$	��...��$	��1 0q�$��14(�
����������	#FFD6D6D6���	#FFD6D6D6 J� 	AllowDrop .   SelectFileButtonStyle#���  $��379,-12,-256,31q�]� $��1��$��2��$��1��$��0,0,20,5q���.  .+��� $��76��$��14(�$��
-180,6,0,0q�$��
Password :��$��27��$	��Top=�G� -    G�Name$ PasswordTextBox��$	��Top=�$��33��$��Center=� " RoundedGlowingPasswordBoxStyle#���  $ Password��$��2��$ 0.524,-0.843&�$��
-57,0,16,0q�{��� *IsAvailable+�� $
��Left=�$
��10px(�
�������  LinkLabelStyle#���  |�. $��5��$	��260��$ 0.412,0.611&�.���� $3��-This password will be used all team members. ��$��	-44,0,0,0q�#���  $	��245��$��12(�$��18���� $
��Left=�$
��10px(�
�������$��-110 20 0 0q�#���  |�. $��5���� $��1��$��2��$ Vertical=�$��0 0 20 0q�$��2��.+��� $ 
Horizontal=�$��	-36,0,0,0q�$��27��{��� *IsAvailable+.+��� -    ��Name$ RememberCheckBox��$��I am a administrator��$��Center=�$��
-85,1,50,1q�
���iii�  RoundedCheckBoxStyle#���  $��4�� d� 	IsChecked$ FalseY�$	��159���� -	    ��Name$ LoginButton��$��LOG IN��  RoundedButtonStyle#���  $
��25 5q�$��SemiBold"�
����������	#FF40A6D1$��14(�$��3��$��97��$��28��$��	75,0,0,-1q���Z� 