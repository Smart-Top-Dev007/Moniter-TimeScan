   M S B A M L   `   `   `  ���� K  GMonitor.TaskView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Monitor.TaskView.ViewMonitor.TaskView.View  E'clr-namespace:Monitor.TaskView.ResourceMonitor.TaskView.Resource  )    #Monitor.TaskView.View.SettingWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5local#clr-namespace:Monitor.TaskView.View     <controls'clr-namespace:Monitor.TaskView.Resource     -   $	��390��$	��300��  � 
ResizeMode$  NoResize=�$	��390��$��508.28�� � WindowStartupLocation$ CenterScreen=� � Icon$7 1/Monitor.TaskView;component/Resource/TaskView.ico��
���   �.���  � ColumnDefinitions �� $	��51* ��� ����  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ #eeeeee�� � Offset$ 0Q�� $ #c3c3c3��$ 1Q�� $ #eeeeee��$ 0.866Q� � RowDefinitions �� $��50 ��� .+�� -   $��50��$	��Top=����	#FF40A6D1$��2��	 J� Effect	 3  -System.Windows.Media.Effects.DropShadowEffect  
   
BlurRadius$
 10Q�   ShadowDepth$ 3Q�   	Direction$	 270Q�   Opacity$	 0.2Q� �� $
��Auto ��� �� $
��Auto ��� $��60 �.+�0   *Monitor.TaskView.Resource.HeaderDecoration  ]� $��2��$��0��$
��Left=�$��50����.  .+��� -    ��Name$ AVARTA��$��25��$��25�� K� ZIndex$ 1��$
��Left=�$��	14,0,0,10q�$��/Resource/net-on.png��$��Uniform=� J� RenderTransformOrigin$ 0.396,1.005&�$��Bottom=��� $�� Setting��$��Center=�$��Center=�
�������$��SemiBold"�$��21(� J� IsManipulationEnabled . $��0,5,5,5q�$��0,0,0,0q��� $��2��$��2��$��Center=�$��Right=�$��0,0,5,0q�.+��� -   $��40��$��40��$	����   CloseMinMaxButtonStyle#���   
������ 
������ 
�������$.��(pack://application:,,,/Font/#FontAwesome)�$��20(�$��Bottom=�|�.  � $��0��$��1��$��Stretch=�$��Stretch=�$��2�� �� $��* ��� $��212.714 ��� $��51.286 � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* �.+��� -    ��Name$ ServerIP��$��1��$��2��$��0��$��
0,20,20,20q�$��35��$��Center=�   RoundedGlowingIPTextBoxStyle#���   � ToolTip$ 	Server IP��$��1��$��14(�{��� *IsAvailable+�� -   $ Username��$��1��$��2��$��1��$��	0,0,20,20q�$��35��$��Center=�  RoundedGlowingTextBoxStyle#���  $ 	User Name��$��1��$��14(�{��� *IsAvailable+�� -   $ Company��$��1��$��2��$��2��$��	0,0,20,20q�$��35��$��Center=�#���  $ Company��$��1��$��14(�{��� *IsAvailable+�� -   $ WorkDirectory��$��Center=�  SelectFileTextBoxStyle#���  ���	#FFFFFFFF���	#FFABADB3$��1q�$��14(�
���   �$ Work Directory��$��1��$��1��$��3��{��� *IsAvailable+�� -	   $��Right=�$	��...��$	��1 0q�$��14(�
����������	#FFD6D6D6���	#FFD6D6D6 J� 	AllowDrop .   SelectFileButtonStyle#���  $��2��$��0,0,20,0q�$��3���� -
   $ Version�� �� 
IsReadOnly . $��1��$��2��$��4��$��0,10,20,164q�$��35��$��Center=�#���  $ Version��$��1��$��14(�{�.  �� -   $ Width�� . $��1��$��2��$��4��$��0,50,20,104q�$��35��$��Center=�#���  $ Width��$��1��$��14(�{�.  �� -   $ Height�� . $��1��$��2��$��4��$��0,101,20,53q�$��35��$��Center=�#���  $ Height��$��1��$��14(�{�.  �� $��1��$��4�� �� Orientation$ Vertical=�$��
0,146,20,0q�$��2��.+��� -    ��Name$ ResetButton��$	��Set��  RoundedButtonStyle#���  $��0,19,0,3q�$��Right=�$
��25 5q�$��SemiBold"�
����������	#FF40A6D1$��14(�$��3��$ 1.779,0.903&���Z� �� $��Server IP :��$��Center=�$��
30,23,12,0q�$	��Top=�$��Right=�$��Center=�$��35��$��78���� $��User Name :��$��Center=�$��	30,3,12,0q�$	��Top=�$��Right=�$��Center=�$��35��$��78��$��1���� $��	Company :��$��Center=�$��	17,0,13,0q�$	��Top=�$��Right=�$��Center=�$��35��$��90��$��2���� $��Work Directory :��$��Center=�$��6,0,13,0q�$	��Top=�$��Right=�$��Center=�$��35��$	��101��$��3���� $��	Version :��$��Center=�$��
51,10,13,0q�$	��Top=�$��Right=�$��Center=�$��35��$��56��$��4��$ 0.482,0.457&��� $��Capture Width :��$��Center=�$��
11,62,12,0q�$	��Top=�$��Right=�$��Center=�$��35��$��97��$��4��$ 1.033,0.548&��� $��Capture Height :��$��Center=�$��10,113,13,0q�$	��Top=�$��Right=�$��Center=�$��35��$��97��$��4��$ 1.033,0.548&�