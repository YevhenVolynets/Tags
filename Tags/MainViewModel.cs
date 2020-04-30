using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Tags
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            Init();
            SelectedCommand = new Command<TagViewModel>(TagSelected);
        }

        private void TagSelected(TagViewModel tagViewModel)
        {
            //TagLevels.Has(tagViewModel.Level + 1);
            if (!tagViewModel.IsSelected)
            {
                foreach (var tagVM in TagLevels[tagViewModel.Level].Tags)
                {
                    tagVM.IsSelected = false;
                }
                tagViewModel.IsSelected = true;

                if (tagViewModel.Tag.Tags == null || !tagViewModel.Tag.Tags.Any())
                    return;
                var nextLevel = new TagsLevelViewModel();
                foreach (var tag in tagViewModel.Tag.Tags)
                {
                    var tmv = new TagViewModel(tag, tagViewModel.Level + 1);
                    tmv.ParentId = tagViewModel.Id;
                    nextLevel.Tags.Add(tmv);
                }
                TagLevels.Add(nextLevel);
            }
            else
            {
                tagViewModel.IsSelected = false;
            }
        }

        public List<Tag> OriginalTags { get; set; }

        public ICommand SelectedCommand { get; set; }

        public ObservableCollection<TagsLevelViewModel> TagLevels { get; set; }

        public void Init()
        {
            var subT1 = new Tag
            {
                Text = "ST1"
            };
            var subT2 = new Tag
            {
                Text = "ST2"
            };
            var tag1 = new Tag
            {
                Text = "MT1",
                Tags = new List<Tag> { subT1, subT2 }
            };
            var tag2 = new Tag
            {
                Text = "MT2"
            };
            OriginalTags = new List<Tag> { tag1, tag2 };
            TagLevels = new ObservableCollection<TagsLevelViewModel>();
            var firstLevel = new TagsLevelViewModel();
            foreach (var tag in OriginalTags)
            {
                var tagViewModel = new TagViewModel(tag, 0);
                firstLevel.Tags.Add(tagViewModel);
            }
            TagLevels.Add(firstLevel);
        }

    }

    public class TagViewModel
    {
        public Guid Id { get; }
        public Guid? ParentId { get; set; }
        public string Text { get; set; }
        public bool IsSelected { get; set; }
        public int Level { get; set; }
        public Tag Tag { get; }
        public TagViewModel(Tag tag, int lvl)
        {
            Tag = tag;
            Text = tag.Text;
            Level = lvl;
            Id = Guid.NewGuid();
        }
    }

    public class TagsLevelViewModel
    {
        public ObservableCollection<TagViewModel> Tags { get; }
        public TagsLevelViewModel()
        {
            Tags = new ObservableCollection<TagViewModel>();
        }
    }
}

